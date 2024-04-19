-- DROP all TABLES
DROP TABLE IF EXISTS [user_credential];
DROP TABLE IF EXISTS degree_job_person;
DROP TABLE IF EXISTS degree;
DROP TABLE IF EXISTS job;
DROP TABLE IF EXISTS person;

-- USER_CREDENTIALS TABLE
CREATE TABLE user_credential (
	username VARCHAR(50) NOT NULL UNIQUE,
	password VARCHAR(64) NOT NULL,
	CONSTRAINT pk_user_credential
		PRIMARY KEY (username)
);

-- PERSON TABLE
CREATE TABLE person (
	person_id INT IDENTITY NOT NULL UNIQUE,
	first_name VARCHAR(35) NOT NUlL, 
	last_name VARCHAR(35) NOT NULL,
	email VARCHAR(70),
	phone VARCHAR(15),
	CONSTRAINT pk_person
		PRIMARY KEY (person_id)
);
	
-- DEGREE TABLE
CREATE TABLE degree (
	degree_id INT IDENTITY NOT NULL UNIQUE,
	[type] VARCHAR(20) NOT NULL, 
	program VARCHAR(50), 
	major VARCHAR(50),
	concentration VARCHAR(50),
	year_awarded SMALLINT,
	person_id INT NOT NULL,
	CONSTRAINT pk_degree
		PRIMARY KEY (degree_id)
);

-- JOB TABLE
CREATE TABLE job (
	job_id INT IDENTITY NOT NULL UNIQUE, 
	job_title VARCHAR(50) NOT NULL,
	business_name VARCHAR(50),
	salary DECIMAL(13,2),
	description TEXT,
	person_id INT NOT NULL,
	CONSTRAINT pk_job
		PRIMARY KEY (job_id)
);

-- DEGREE_JOB_PERSON TABLE
CREATE TABLE degree_job_person (
	person_id INT NOT NULL,
	job_id INT NOT NULL,
	degree_id INT NOT NULL,
	CONSTRAINT pk_degree_job
		PRIMARY KEY (person_id, job_id, degree_id)
);

-- FOREIGN KEY CONSTRAINTS
-- Degree person_id
ALTER TABLE degree
	ADD CONSTRAINT fk_degree_person
	FOREIGN KEY (person_id)
	REFERENCES person(person_id)
;

-- Job person_id
ALTER TABLE job
	ADD CONSTRAINT fk_job_person
	FOREIGN KEY (person_id)
	REFERENCES person(person_id)
;

-- Degree_Job_Person degree_id
ALTER TABLE degree_job_person
	ADD CONSTRAINT fk_degree_job_person_degree
	FOREIGN KEY (degree_id)
	REFERENCES degree(degree_id)
;

-- Degree_Job_Person job_id
ALTER TABLE degree_job_person
	ADD CONSTRAINT fk_degree_job_person_job
	FOREIGN KEY (job_id)
	REFERENCES job(job_id)
;

-- Degree_Job_Person person_id
ALTER TABLE degree_job_person
	ADD CONSTRAINT fk_degree_job_person_person
	FOREIGN KEY (person_id)
	REFERENCES person(person_id)
;

-- Default User credentials
INSERT INTO user_credential 
    (username, password)
    VALUES
    ('admin', '0c9ec37f13454b6ef9c1c93f88814906e09a3b2dd9ec83eb8e39588216590aca')
;