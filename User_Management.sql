create table UserInfo(
UserId int AUTO_INCREMENT,
UserName varchar(60) not null,
UserEmailId varchar(100) not null,
Age int,
PhoneNumber varchar(15),
badgeIds varchar(500),
genres varchar(200),
inUse tinyint,
crt_ts date,
crt_user varchar(30),
updt_ts date,
updt_user varchar(30),
PRIMARY KEY(UserId)
);


create table BadgeInfo(
BadgeId int AUTO_INCREMENT,
BadgeName varchar(60) not null,
BadgeDescription varchar(400),
crt_ts date,
crt_user varchar(30),
updt_ts date,
updt_user varchar(30),
PRIMARY KEY(BadgeId)
);

select * from usr_mgmt.UserInfo;
select * from BadgeInfo;
select utc_timestamp();
/*
insert into BadgeInfo(BadgeName,BadgeDescription,crt_ts,crt_user,updt_ts,updt_user) values ('Level 0','Start of Tech journey',utc_timestamp(),'Admin',utc_timestamp(),'Admin');
insert into usr_mgmt.UserInfo(UserName, UserEmailId, Age, PhoneNumber,badgeIds,genres, inUse, crt_ts, crt_user, updt_ts,updt_user)
values 				('Inder', 'inder@inder.com', 25,'83390411902','1','C#,Cloud,AWS', 0,utc_timestamp(),'Admin',utc_timestamp(),'Admin')
*/

select UserName, UserEmailId, Age, PhoneNumber,badgeIds,genres from usr_mgmt.UserInfo where UserEmailId like 'inder@inder.com'