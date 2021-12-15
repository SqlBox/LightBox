using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    class PostgreSqlStatementParser : SqlStatementParser
    {
        public PostgreSqlStatementParser(string originalSqlRef) : base(originalSqlRef)
        {
        }

        ~PostgreSqlStatementParser() 
        { 
        }

        unsafe internal override void determineStatementRanges(char* sql, int length, string initial_delimiter, List<StatementRange> ranges, string line_break)
        {
            bool is_inside_block = false;
            bool _stop = false;
            string delimiter = string.IsNullOrEmpty(initial_delimiter) ? ";" : initial_delimiter;
            char* delimiter_head;
            fixed (char* delimiter_head_alloc = delimiter)
            {
                delimiter_head = delimiter_head_alloc;
            }
            char* start = (char*)(sql);
            char* head = sql;
            char* tail = head;
            char* safe_exit = head;
            char* end = head + length;
            char* new_line;
            fixed (char* new_line_alloc = line_break)
            {
                new_line = new_line_alloc;
            }
            bool have_content = false; // Set when anything else but comments were found for the current statement.
            //int statementStart = 0;
            int currentLine = 0;
            while (!_stop && tail < end && safe_exit < end)
            {
                switch (*tail)
                {
                    case '/': // Possible multi line comment or hidden (conditional) command.
                        if (*(tail + 1) == '*')
                        {
                            tail += 2;
                            bool is_hidden_command = (*tail == '!');
                            while (true)
                            {
                                while (tail < end && *tail != '*')
                                    tail++;
                                if (tail == end) // Unfinished comment.
                                    break;
                                else
                                {
                                    if (*++tail == '/')
                                    {
                                        tail++; // Skip the slash too.
                                        break;
                                    }
                                }
                            }

                            if (!is_hidden_command && !have_content)
                                head = tail; // Skip over the comment.
                        }
                        else
                            tail++;

                        break;

                    case '-': // Possible single line comment.
                        {
                            char* end_char = tail + 2;
                            if (*(tail + 1) == '-')
                            {
                                // Skip everything until the end of the line.
                                tail += 2;
                                while (tail < end && !isLineBreak(tail, new_line))
                                    tail++;
                                if (!have_content)
                                    head = tail;
                            }
                            else
                                tail++;

                            break;
                        }
                    case '"':
                    case '\'':
                    case '`': // Quoted string/id. Skip this in a local loop.
                        {
                            have_content = true;
                            char quote = *tail++;
                            while (tail < end && *tail != quote)
                            {
                                // Skip any escaped character too.
                                if (*tail == '\\')
                                    tail++;
                                tail++;
                            }
                            if (*tail == quote)
                                tail++; // Skip trailing quote char to if one was there.

                            break;
                        }
                    case '$':
                        {
                            if(!is_inside_block)
                            {
                                have_content = true;
                                //Possible start of a block code
                                //run and find the next closing $ and keep the keyword
                                char* run = tail;
                                char* end_char = tail + 2;
                                if (*(tail + 1) == '$' && (*end_char == ' ' || *end_char == '\t' || isLineBreak(end_char, new_line) || *end_char == '\r'))
                                {
                                    //case of $$
                                    delimiter = "$$";
                                    fixed (char* delimiter_head_alloc = delimiter)
                                    {
                                        delimiter_head = delimiter_head_alloc;
                                    }
                                    is_inside_block = true;
                                    tail += delimiter.Length;
                                }
                                else if (*(tail + 1) != 48)//cant be leading zero after $
                                {
                                    //scan for the block word
                                    StringBuilder delimiterBuilder = new StringBuilder();
                                    delimiterBuilder.Append(*run);//append first $
                                    run++;
                                    bool is_block_valid = true;
                                    while (run < end && *run != '\n' && *run != '\0')
                                    {
                                        bool is_dollar = *run == 36;
                                        bool is_number = *run >= 48 && *run <= 57;
                                        bool is_cap = *run >= 65 && *run <= 90;
                                        bool is_underscore = *run == 95;
                                        bool is_lower = *run >= 97 && *run <= 122;
                                        bool is_extended_set = *run >= 128;
                                        if (is_dollar || is_number || is_cap || is_underscore || is_lower || is_extended_set)
                                        {
                                            delimiterBuilder.Append(*run);
                                            if (is_dollar)
                                            {
                                                run++;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            is_block_valid = false;
                                        }
                                        run++;
                                    }
                                    if (is_block_valid)
                                    {
                                        delimiter = delimiterBuilder.ToString();
                                        fixed (char* dhead = delimiter)
                                        {
                                            delimiter_head = dhead;
                                        }
                                        is_inside_block = true;
                                    }
                                    tail += delimiter.Length;
                                }
                            } else
                            {
                                tail++;
                            }
                            break;
                        }
                    default:
                        if (isLineBreak(tail, new_line))
                        {
                            ++currentLine;
                        }
                        if (*tail > ' ')
                            have_content = true;
                        tail++;
                        break;
                }

                if (*tail == *delimiter_head)
                {
                    // Found possible start of the delimiter. Check if it really is.
                    int count = delimiter.Length;
                    if (count == 1 && !is_inside_block)
                    {
                        // Most common case. Trim the statement and check if it is not empty before adding the range.
                        head = skip_leading_whitespace(head, tail);
                        if (head < tail)
                        {
                            long startT = head - (char*)sql;
                            long endT = tail - head;
                            if (includeInRange(startT, endT))
                            {
                                ranges.Add(new StatementRange(startT, endT));
                            }
                        }
                        head = ++tail;
                        have_content = false;
                    }
                    else if(is_inside_block)
                    {
                        char* run = tail + 1;
                        char* del = delimiter_head + 1;
                        while (count-- > 1 && (*run++ == *del++))
                            ;
                        if (count == 0)
                        {
                            tail += delimiter.Length;
                            fixed (char* delimiter_head_alloc = initial_delimiter)
                            {
                                delimiter_head = delimiter_head_alloc;
                                delimiter = initial_delimiter;
                            }
                            is_inside_block = false;
                            //In case the character without space after $$ is ; ex. $$<<code block>>$$;
                            if(*tail == *delimiter_head)
                            {
                                count = delimiter.Length;
                                if (count == 1 && !is_inside_block)
                                {
                                    // Most common case. Trim the statement and check if it is not empty before adding the range.
                                    head = skip_leading_whitespace(head, tail);
                                    if (head < tail)
                                    {
                                        long startT = head - (char*)sql;
                                        long endT = tail - head;
                                        if (includeInRange(startT, endT))
                                        {
                                            ranges.Add(new StatementRange(startT, endT));
                                        }
                                    }
                                    head = ++tail;
                                    have_content = false;
                                }
                            }
                        } 
                    }
                }
                safe_exit++;
            }

            // Add remaining text to the range list.
            head = skip_leading_whitespace(head, tail);
            if (head < tail)
            {
                long startT = head - (char*)sql;
                long endT = tail - head;
                if (includeInRange(startT, endT))
                {
                    ranges.Add(new StatementRange(startT, endT));
                }
            }
        }

    }
}
