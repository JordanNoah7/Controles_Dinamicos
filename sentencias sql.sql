SELECT T.name as dato, C.object_id as id, C.name as columna FROM Prueba.sys.all_columns C join sys.systypes T ON C.user_type_id = T.xusertype WHERE object_id = 581577110

SELECT object_id, name FROM Prueba.sys.tables

select t.name, c.* from sys.syscolumns c join sys.systypes t on (c.xtype = t.xtype)
where id = 274100017

select * from prueba.sys.all_columns

select * from sys.systypes

alter table Prueba.dbo.Table_1 alter column Nacimiento date