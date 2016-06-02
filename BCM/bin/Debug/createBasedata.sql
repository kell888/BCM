declare @sql nvarchar(1000)
if not exists(select * from dbo.sysobjects where id = object_id(N'[dbo].[CreateBaseData]') and type=N'P')
	begin
set @sql = N'create procedure CreateBaseData
(
 @strsql ntext
 )
 as
 begin
	if not exists(select * from dbo.sysobjects where id = object_id(N''[dbo].[p_test_config_table]'') and type=N''P'')
	begin
        execute(@strsql)
    end
    exec dbo.p_test_config_table
 end'
 execute(@sql)
 end