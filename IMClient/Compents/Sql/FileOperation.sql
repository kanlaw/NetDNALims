DROP TABLE IF EXISTS "main"."FileOperation";
CREATE TABLE "FileOperation" (
"id"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"fileName"  TEXT NOT NULL,
"filePath"  TEXT NOT NULL,
"fileSize"  INTEGER NOT NULL,
"deleted"  INTEGER NOT NULL,
"Operation"  INTEGER,
"fileId"  TEXT,
"fromUser"  TEXT,
"toUser"  TEXT
);