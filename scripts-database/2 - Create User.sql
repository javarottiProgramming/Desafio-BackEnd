
-- Role: user_challenge
DROP ROLE IF EXISTS user_challenge;

CREATE ROLE user_challenge WITH
  LOGIN
  SUPERUSER
  INHERIT
  CREATEDB
  CREATEROLE
  REPLICATION
  BYPASSRLS
  ENCRYPTED PASSWORD 'SCRAM-SHA-256$4096:HzGFBISvVvZXLsixPSyKKQ==$18CLREfBLRsaKiI98xes4NnCuxwqrZLmGKtpvHijy1U=:SQJL0di8xLTK/h/qFwPq7D0zlMD0AEu+p/sUCF/g51M=';

GRANT ALL ON SCHEMA public TO user_challenge;