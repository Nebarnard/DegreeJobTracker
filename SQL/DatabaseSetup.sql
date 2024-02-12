-- DROP DATABASE
DROP DATABASE IF EXISTS DegreeJobTracker;

-- CREATE DATABASE
CREATE DATABASE DegreeJobTracker;

-- USING DATABASE
GO
USE DegreeJobTracker;

-- DROP all TABLES
DROP TABLE IF EXISTS [user];
DROP TABLE IF EXISTS degree_job;
DROP TABLE IF EXISTS degree;
DROP TABLE IF EXISTS job;
DROP TABLE IF EXISTS person;

-- USER TABLE

-- PERSON TABLE
CREATE TABlE person (
	person_id IDENTITY INT NOT NULL UNIQUE,
	first_name VARCHAR(35) NOT NUlL, 
	last_name VARCHAR(35) NOT NULL,
	email VARCHAR(70),
	phone (VARCHAR(15),
	CONSTRAINT pk_person
		PRIMARY KEY (person_id)
);
	
-- DEGREE TABLE
CREATE TABLE degree (
	degree_id IDENTITY INT NOT NULL UNIQUE,
	[type] VARCHAR(20) NOT NULL, 
	program VARCHAR(50), 
	major VARCHAR(50,
	concentration VARCHAR(50),
	year_awarded SMALLINT,
	person_id NOT NULL,
	CONSTRAINT pk_degree
		PRIMARY KEY (degree_id)
);

-- JOB TABLE
CREATE TABLE job {
	person_id INT NOT NULL, 
	job_title VARCHAR(50) NOT NULL,
	business_name VARCHAR(50),
	salary DECIMAL(13,2),
	description TEXT,
	CONSTRAINT pk_job
		PRIMARY KEY (person_id, job_title)
);

-- DEGREE_JOB TABLE