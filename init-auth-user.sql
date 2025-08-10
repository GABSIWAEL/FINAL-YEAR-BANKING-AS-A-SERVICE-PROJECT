-- ./init-auth-user.sql

CREATE USER IF NOT EXISTS 'appuser'@'%' IDENTIFIED BY 'apppassword';
GRANT ALL PRIVILEGES ON authdb.* TO 'appuser'@'%';
FLUSH PRIVILEGES;
