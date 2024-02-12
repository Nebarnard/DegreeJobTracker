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
	job_id IDENTITY INT NOT NULL UNIQUE,
	person_id INT NOT NULL, 
	job_title VARCHAR(50) NOT NULL,
	business_name VARCHAR(50),
	salary DECIMAL(13,2),
	description TEXT,
	CONSTRAINT pk_job
		PRIMARY KEY (job_id)
);

-- DEGREE_JOB TABLE
CREATE TABLE degree_job (
	person_id INT NOT NULL,
	job_title VARCHAR(50) NOT NULL,
	degree_id INT NOT NULL,
	CONSTRAINT pk_degree_job
		PRIMARY KEY (person_id, job_title, degree_id)
);

-- FOREIGN KEY CONSTRAINTS
-- Degree person_id
ALTER TABLE degree
	ADD CONSTRAINT fk_degree_person
	FOREIGN KEY (person_id)
	REFERENCES person(person_id)
;

--Job person_id
ALTER TABLE job
	ADD CONSTANT fk_job_person
	FOREIGN KEY (person_id)
	REFERENCES person(person_id)
;

--Degree_Job job_id
ALTER TABLE degree_job
	ADD CONSTANT fk_degree_job_job_id
	FOREIGN KEY (job_id)
	REFERENCES job(job_id)
;

-- Degree_Job degree_id
	ADD CONSTANT fk_degree_job_degree_id
	FOREIGN KEY (degree_id)
	REFERENCES degree(degree_id)
;

-- SEED DATA

-- Person table
INSERT INTO person
	(first_name, last_name)
VALUES 
	('Amber', 'Fields'),
	('Maria', 'Miller'),
	('Chris', 'Perez'),
	('Ryan', 'Rupard'),
	('Allie', 'Hodges'),
	('Seth', 'Kerney'),
	('Lacey', 'Ketron'),
	('Nayland', 'Prince'),
	('Derek', 'Dunsmore'),
	('Amy', 'Koenig'),
	('Hannah', 'Mills'),
	('Malik', 'Kyle'),
	('Ariel', 'Lowery'),
	('Mollie', 'Owens'),
	('Kelly', 'Ayers'),
	('Dustin', 'Hurst'),
	('David', 'Self'),
	('Carissa', 'Helton'),
	('Laura', 'Burchfield'),
	('Kelly', 'Cordle'),
	('Ethan', 'Oakes'),
	('Bristoe', 'Bible'),
	('Gregory', 'Payne'),
	('Stephanie', 'Derochers'),
	('Niral', 'Patel'),
	('Felicia', 'McCarroll'),
	('Carolyn', 'Oathout')
;

