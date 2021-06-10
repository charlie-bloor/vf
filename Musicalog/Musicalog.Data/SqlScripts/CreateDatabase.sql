create database Musicalog collate SQL_Latin1_General_CP1_CI_AS
go

use Musicalog
go

create table Album
(
    Id int identity
        constraint Album_pk
        primary key nonclustered,
    Title varchar(256) not null,
    ArtistName varchar(256) not null,
    Stock int not null,
    constraint UIX_Title_ArtistName
        unique (Title, ArtistName)
)
    go
