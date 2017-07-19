DROP TABLE IF EXISTS "main"."Recent_Link";
CREATE TABLE "Recent_Link" (
"id"  TEXT NOT NULL,
"SelfUserId"  TEXT,
"SelfRealName"  TEXT,
"linkUserId"  TEXT,
"linkRealName"  TEXT,
PRIMARY KEY ("id" ASC)
);