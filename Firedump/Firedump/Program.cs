using Firedump.core.parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Firedump
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            string sql = @"Select * from table1; 
                COMMIT;
DELIMITER   $$   
USE `moviecould`;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ptest3`()
BEGIN

	select * from fasts limit 3;
    select * from fasts limit 3;
END     $$      
DELIMITER ;  
;
insert t set something;
DELIM;";

            SqlStatementParserWrapper parser = new SqlStatementParserWrapper(sql,sqlitetables.DbTypeEnum.MYSQL);
            List<StatementRange> results = parser.Parse();
            foreach(StatementRange p in results)
            {
                Console.WriteLine("--------SQL---------");
                Console.WriteLine("START:" + (int)p.start);
                Console.WriteLine("END:" + (int)p.end);
                Console.WriteLine(sql.Substring((int)p.start, (int)p.end));
                //Console.WriteLine(sql.Substring(40, 6));
            }
            //Application.EnableVisualStyles();
            //Application.Run(new MainHome());
        }

    }
}
