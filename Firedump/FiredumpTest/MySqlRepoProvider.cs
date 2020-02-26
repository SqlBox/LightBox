using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiredumpTest
{
    public class MySqlRepoProvider
    {
        public static IEnumerable statementProvider()
        {
            yield return new TestCaseData(@"select * from table c; ; ; ; ", 1);
            yield return new TestCaseData(@"select * from mytable; create table2(`id` not null /* ;ignored semicolon;*/ );;    ;     ;; ;; ;    ;   " +
                "; \n select * from table1 where t.prop like 'commnet;' and id = 3 #trailing; comment;;;;;",3);
            yield return new TestCaseData(@"select * from something      update asd set asd = 3", 1);
            yield return new TestCaseData(@"Select * from table1; 
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
                DELIM;",5);
        }
    }
}
