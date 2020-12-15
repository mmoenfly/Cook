-- Examples for queries that exercise different SQL objects implemented by this assembly

-----------------------------------------------------------------------------------------
-- Stored procedure
-----------------------------------------------------------------------------------------
-- exec StoredProcedureName


-----------------------------------------------------------------------------------------
-- User defined function
-----------------------------------------------------------------------------------------
-- select dbo.FunctionName()


-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- CREATE TABLE test_table (col1 UserType)
-- go
--
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 1'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 2'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 3'))
--
-- select col1::method1() from test_table



-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- select dbo.AggregateName(Column1) from Table1
select  dbo.XmlShred( '<?xml version="1.0" encoding="UTF-8" standalone="no"?><PCID><PC id=".68QGDN1.CN486430920007." unid="2A29D9C35BD99B77852578430075737B" email="evincent@cookconsulting.net" product="01RV5R" os="Microsoft Windows NT 5.1.2600 Service Pack 3" officeversion="12" targetdir="C:\AS400DA\" systemname="EMILYDELL6500" /></PCID>','systemname')
 