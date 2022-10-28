SELECT T.name as dato, C.object_id as id, C.name as columna FROM Prueba.sys.all_columns C join sys.systypes T ON C.user_type_id = T.xusertype WHERE object_id = 581577110

SELECT object_id, name FROM Prueba.sys.tables

select t.name, c.* from sys.syscolumns c join sys.systypes t on (c.xtype = t.xtype)
where id = 274100017

select * from prueba.sys.all_columns

select * from sys.systypes

alter table Prueba.dbo.Table_1 alter column Nacimiento date

SELECT  f.name AS foreign_key_name  
 ,OBJECT_NAME(f.parent_object_id) AS table_name  
 ,COL_NAME(f.parent_object_id, fc.parent_column_id) AS constraint_column_name  
 ,OBJECT_NAME (f.referenced_object_id) AS referenced_object  
 ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name  
 ,f.is_disabled, f.is_not_trusted 
,f.delete_referential_action_desc  
,f.update_referential_action_desc  
FROM sys.foreign_keys AS f  
INNER JOIN sys.foreign_key_columns AS fc   
ON f.object_id = fc.constraint_object_id   
WHERE f.parent_object_id = OBJECT_ID('dbo.Table_1');

select f.name, f.parent_object_id, col_name(OBJECT_id(fc.parent_object_id, 5), fc.parent_column_id) from Prueba.sys.foreign_key_columns fc join Prueba.sys.foreign_keys f on fc.parent_object_id = f.parent_object_id

select dbid, name from sys.sysdatabases