-- Add your test scenario here --

declare @ret int  
 exec @ret = dbo.loadunid  '207971052391C79B8525780A004AEACA'
select @ret 