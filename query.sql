-- Active: 1668565862197@@127.0.0.1@3306@zoraIdeale

SELECT * FROM `Server`;

SELECT * FROM `Genres`;

SELECT * FROM `BookAvatar`;

SELECT * FROM `BookGenres`;

SELECT * FROM `Book`;

SELECT * FROM `BookDetail`;

SELECT * FROM `BookStatus`;

SELECT * from `Status`;

describe Book;

describe BookDetail;

describe BookGenres;

describe Server;

describe Genres;

describe BookAvatar;

DESCRIBE BookChapter;

DESCRIBE BookChapterServer;

DESCRIBE Image;

DESCRIBE Star;

INSERT INTO
    `BookAvatar`(BookDetailId, ServerId, Url)
VALUES(1, 2, "xxx");