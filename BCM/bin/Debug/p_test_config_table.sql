create procedure p_test_config_table
as
begin

	delete dbo.m_object
	delete dbo.m_station
	delete dbo.m_train
	delete dbo.m_device
	delete dbo.s_rm_type

	declare @line_id int
	declare @line_name varchar(256)
	declare @line_no varchar(10)

	declare @train_id int
	declare @train_no varchar(4)
	declare @device_1 int
	declare @device_2 int
	declare @cabin_no1 varchar(6)
	declare @cabin_no2 varchar(6)

	set @line_no = N'02'
	set @line_name = N'2号线'

	INSERT [dbo].[m_object] ([parent_id], [object_name], [full_name], [object_type_id], [current_value],
		[encounter_id], [device_id], [point_type_id], [flash_time], [alarm_status], [alarm_time], 
		[alarm_msg], [Enable], [order_id]) 
	VALUES (0, @line_name, @line_name, 1, CAST(0.00 AS Numeric(10, 2)), 0, 0, NULL, CAST(0x0000A1F20105819C AS DateTime),
	0, NULL, CONVERT(TEXT, N''), 2, 0)
 
	set @line_id = @@identity

	declare @iifor int
	set @iifor = 1;
	while(@iifor < 41)
	begin
		set @train_no =@line_no + right('0' + cast(@iifor as varchar(2)),2)
		--print(@train_no)
		exec p_test_add_train @line_id, @line_name, @train_no, '02', '04'
	--------------------------------------------
		set @iifor = @iifor + 1
	end

	--declare @Line_name varchar(256)  ---线路名
	declare @parent_str varchar(256)---X号线 object_id
	declare @object_num int      --临时object_id
	declare @m_object_num int    --全局监测站id@identity
	declare @DPS_str varchar(256)  --监测站名
	declare @i_CG_num int  --磁钢数
	declare @i_CAM_num int  --磁钢数
	declare @CAM_IP int   --相机最后一个字节ip


	set @Line_name=N'2号线'
	set @i_CAM_num=7  --相机数量
	set @CAM_IP = 61 --相机最后一个字节ip=67起步

	declare @i_dps int   --dps for循环变量
	set @i_dps=0 
	while (@i_dps<6)
	begin
		if (@i_dps = 0)  
		begin
			set @DPS_str = N'车辆段2d监测站'
			set @i_CG_num = 7
		end
	
		if (@i_dps = 1)  
		begin
			set @DPS_str = N'车辆段3d监测站'
			set @i_CG_num = 2
		end
	
		if (@i_dps = 2) 
		begin
			set @DPS_str = N'高速站监测站'
			set @i_CG_num = 5
		end
	
		if (@i_dps = 3)  
		begin
			set @DPS_str = N'三医院监测站上行监测站'
			set @i_CG_num = 5
		end
	
		if (@i_dps = 4)  
		begin
			set @DPS_str = N'三医院监测站下行监测站'
			set @i_CG_num = 5
		end
	
		if (@i_dps = 5) 
		begin
			set @DPS_str = N'石湖路监测站'
			set @i_CG_num = 5
		end

		set @parent_str=(select object_id from m_object where object_name=@Line_name)
	
			--添加监测站：/*****操作 object 表*******/   //object——type——id
		insert m_object (parent_id,object_name,full_name,object_type_id,flash_time,Enable) 
		values(@parent_str,@DPS_str,@Line_name+'\'+@DPS_str,12,GETDATE(),2)
		set @m_object_num  = @@identity --//全局监测站id
			--//添加监测站/*****操作object表*******/	
			
			--//添加监测站：/*****操作 m_station 表*******/
		insert m_station(object_id,   object_name,   user_station_id)
	       values(@m_object_num,@DPS_str,     @i_dps+1  )
			--//添加监测站：/*****操作m_station表*******/
			

		declare @i_CAM_IP int --相机最后一个字节ip起步临时变量
		set @i_CAM_IP=@CAM_IP --相机最后一个字节ip=67起步
		declare @i int   --for循环变量
		set @i=0;
    
		while ( @i<@i_CAM_num )
		begin
			--//添加相机 	/*****操作 object 表*******/
			insert m_object (parent_id,         object_name,         full_name, object_type_id,   device_type_id,  flash_time,Enable)
					values (@m_object_num,N'相机192.168.1.'+CONVERT(varchar(256),@CAM_IP),@Line_name+'\'+@DPS_str+N'\相机192.168.1.'+CONVERT(varchar(256),@CAM_IP), 3,                105 ,   GETDATE() ,2    )
			set @object_num = @@identity
			--//添加相机 /*****操作object表*******/
			
			
			
			--/*****添加相机 m_device 表*******/
			insert  m_device (object_id,object_name,     device_type_id,       address)
					values (@object_num,  N'相机192.168.1.'+CONVERT(varchar(256),@CAM_IP),   105,       N'camera_192.168.1.'+CONVERT(varchar(256),@CAM_IP)    )
			--/*****添加相机m_device表*******/

			 
			--//添加相机状态 /*****操作 object 表*******/	
			insert m_object (parent_id,         object_name,             full_name,    object_type_id,  device_id,  device_type_id,  point_type_id,flash_time,Enable)
					values (@object_num, N'相机192.168.1.'+CONVERT(varchar(256),@CAM_IP)+N'\状态',@Line_name+'\'+@DPS_str+N'\相机192.168.1.'+CONVERT(varchar(256),@CAM_IP)+N'\状态',     4 ,            @object_num ,    105,              3,          GETDATE(),    2    )
			--//添加相机状态 /*****操作object表*******/


			--//添加相机拍照次数	/*****操作 object 表*******/
			insert m_object (parent_id,         object_name,           full_name,               object_type_id,  device_id,  device_type_id, point_type_id,flash_time,Enable)
					values (@object_num,N'相机192.168.1.'+CONVERT(varchar(256),@CAM_IP)+N'\拍照次数',@Line_name+'\'+@DPS_str+N'\相机192.168.1.'+CONVERT(varchar(256),@CAM_IP)+N'\拍照次数',     5 ,        @object_num,     105 ,           2,            GETDATE() ,    2    )	
			--//添加相机拍照次数	/*****操作object表*******/


   
			--//添加相机触发次数/*****操作 object 表*******/
			insert m_object (parent_id ,            object_name,              full_name,        object_type_id,  device_id,  device_type_id, point_type_id,flash_time,Enable)
					values (@object_num,N'相机192.168.1.'+CONVERT(varchar(256),@CAM_IP)+N'\触发次数',@Line_name+'\'+@DPS_str+N'\相机192.168.1.'+CONVERT(varchar(256),@CAM_IP)+N'\触发次数',     5,         @object_num,     105 ,           1,            GETDATE() ,    2    )
			--//添加相机触发次数/*****操作object表*******/
			 

			set @i=@i+1; 
			set @CAM_IP=@CAM_IP+1			 
		end			 


		set @i=1

		while (@i<=@i_CG_num) --添加磁钢
		begin	 
			--//添加磁钢/*****操作 object 表*******/
			insert m_object (parent_id,       object_name,          full_name,  object_type_id,device_type_id,flash_time,Enable)
					values (@m_object_num,N'磁钢'+CONVERT(varchar(256),@i),@Line_name+'\'+@DPS_str+N'\磁钢'+CONVERT(varchar(256),@i),   3,            104       ,GETDATE() ,    2    )	
			set @object_num = @@identity	 
			--//添加磁钢/*****操作object表*******/
	

			--/*****操作磁钢1 m_device 表*******/
			insert  m_device (object_id,object_name,     device_type_id,       address)
					values (@object_num,N'磁钢'+CONVERT(varchar(256),@i),          104,       'alnico_'+CONVERT(varchar(256),@i)   )
			--/*****操作磁钢1m_device表*******/ 

			 
			--//添加磁钢触发次数/*****操作 object 表*******/	
			insert m_object (parent_id ,        object_name,     full_name,    object_type_id,  device_id,  device_type_id, point_type_id,flash_time,Enable)
					values (@object_num,N'磁钢触发次数',   @Line_name+'\'+@DPS_str+N'\磁钢'+CONVERT(varchar(256),@i)+N'\触发次数',     5,         @object_num,     104 ,           1,            GETDATE() ,    2    )	
			--//添加磁钢触发次数/*****操作object表*******/
		
			set @i=@i+1; 
		end


		set @i_dps=@i_dps+1
	end


	--//受电弓 point点
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 前滑板磨耗
                     values(       101,        0,          3,          3,           17.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 前滑板磨耗位置
                     values(       101,        1,          3,          0.00,           0.00,             0)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 后滑板磨耗
                     values(       101,        2,          3,          3,           17.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 后滑板磨耗位置
                     values(       101,        3,          3,          0,           0.00,             0)                    
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 前滑板缺口
                     values(       101,        4,          3,          0,           20.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 前滑板缺口位置
                     values(       101,        5,          3,          0,           0.00,             0)                     
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 后滑板缺口
                     values(       101,        6,          3,          0,           20.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--// 后滑板缺口位置
                     values(       101,        7,          3,          0,           0.00,             0)                                   
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前滑板中心偏移
                     values(       101,        8,          3,          0,           0.00,             0)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//后滑板中心偏移
                     values(       101,        9,          3,          0,           0.00,             0)                    
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//受电弓中心偏移
                     values(       101,        10,          3,          -30,           30.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前滑板上下倾斜
                     values(       101,        11,          3,          0,           0.00,             0)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//后滑板上下倾斜
                     values(       101,        12,          3,          0,           0.00,             0)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//受电弓上下倾斜
                     values(       101,        13,          3,          -40,           40.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前后倾斜量
                     values(       101,        14,          3,          -40,           40.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//球铰高度	
                     values(       101,        15,          50,          850,           1000.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前滑板左羊角x值
                     values(       101,        16,          10,          240,           280.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前滑板左羊角y值
                     values(       101,        17,          10,          180,           230.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前滑板右羊角x值
                     values(       101,        18,          10,          240,           280.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前滑板右羊角y值
                     values(       101,        19,          10,          180,           230.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//后滑板左羊角x值 
                     values(       101,        20,          10,          240,           280.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//后滑板左羊角y值
                     values(       101,        21,          10,          180,           230.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//后滑板右羊角x值 
                     values(       101,        22,          10,          240,           280.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//后滑板右羊角y值
                     values(       101,        23,          10,          180,           230.00,             2)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//前滑板高 
                     values(       101,        24,          3,          3,           17.00,             0)
	insert into dbo.s_rm_type (device_type_id,point_type_id,mutation,normal_low_alarm,normal_high_alarm,enable)--//后滑板高
                     values(       101,        25,          3,          3,           17.00,             0)
                     
	--/*****操作相机拍照次数 s_rm_type 表*******/
	insert  s_rm_type (device_type_id,point_type_id,     normal_low_alarm, normal_high_alarm ,     enable)
           values (105,       1,                           2 ,   2,             2    )
	--/*****操作相机拍照次数s_rm_type表*******/	

	--/*****操作相机触发次数 s_rm_type 表*******/
	insert  s_rm_type (device_type_id,point_type_id,     normal_low_alarm, normal_high_alarm,       enable)
          values (105,       2,                  2,   2,              2    )
	--/*****操作相机触发次数s_rm_type表*******/	
end