create database Musicalog collate SQL_Latin1_General_CP1_CI_AS
go

use Musicalog
go


-- In a real application, we'd likely have the database in third normal form.
-- Artist would probably have its own table, which Album would reference.

create table Album
(
    Id int identity
        constraint Album_pk
        primary key nonclustered,
    ArtistName varchar(128) not null,
    MediaType varchar(128) not null,
    Stock int not null,
    Title varchar(128) not null,
    constraint UIX_Title_ArtistName
        unique (Title, ArtistName)
)
    go

