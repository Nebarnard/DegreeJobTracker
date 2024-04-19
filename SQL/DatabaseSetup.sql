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

-- SEED DATA
-- Real People and Degrees, Fake Salaries.

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

-- Job table
INSERT INTO job
	(person_id, job_title, business_name, salary, description)
VALUES
	(1, 'Cash Reconciliation Specialist', 'Charter Foods', 50000, 'Responsible for ensuring accurate and timely reconciliation of cash transactions within Charter Foods.'),
    (2, 'Staff Accountant', 'NC CPA Firm', 55000, 'Responsible for maintaining financial records, preparing financial reports, and analyzing financial data for clients at NC CPA Firm.'),
    (3, 'Lecturer and MACC Program Coordinator', 'UTK', 60000, 'Responsible for teaching courses and coordinating the Master of Accountancy (MACC) program at UTK.'),
    (4, 'Accounts Receivable Clerk', 'World Choice Investments', 45000, 'Responsible for processing invoices, monitoring accounts receivable, and ensuring timely payments at World Choice Investments.'),
    (5, 'Audit Intern', 'LBMC', 40000, 'Assisting audit teams in conducting financial audits and providing support in analyzing financial statements at LBMC.'),
    (6, 'Coordinator in Instructional Design', 'WSCC', 50000, 'Responsible for designing and implementing instructional materials and programs at WSCC.'),
    (7, 'Assoc Program Planner & Control Analyst', 'Textron Systems', 55000, 'Responsible for planning and controlling program schedules, budgets, and resources at Textron Systems.'),
    (8, 'Tax Staff Accountant', 'LBMC', 55000, 'Responsible for preparing and reviewing tax returns, conducting tax research, and assisting with tax planning at LBMC.'),
    (9, 'Business Integration Analyst', 'Pilot', 60000, 'Responsible for analyzing business processes and systems, identifying opportunities for improvement, and implementing integration solutions at Pilot.'),
    (10, 'Esthetician', 'Self-employed', 40000, 'Providing skincare treatments and services to clients as a self-employed Esthetician.'),
    (11, 'Senior Accountant', 'WSCC', 65000, 'Responsible for overseeing financial operations, preparing financial statements, and providing financial analysis and advice at WSCC.'),
    (12, 'Director of Acquisitions and Asset Management', 'Safe Harbor Investments', 80000, 'Responsible for identifying and evaluating investment opportunities, negotiating acquisitions, and managing assets at Safe Harbor Investments.'),
    (13, 'Fraud Ops Specialist', 'Citi', 55000, 'Responsible for identifying and investigating fraudulent activities, implementing fraud prevention measures, and minimizing financial losses at Citi.'),
    (14, 'Director of Bookkeeping', 'NHC Holston Health and Rehab', 70000, 'Responsible for overseeing bookkeeping operations, managing financial records, and ensuring compliance with accounting standards at NHC Holston Health and Rehab.'),
    (15, 'Supervisor, Talent Acquisition', 'Dollywood', 60000, 'Responsible for leading talent acquisition efforts, recruiting and hiring employees, and implementing recruitment strategies at Dollywood.'),
    (16, 'General Manager', 'Riverstone Resort and Spa', 75000, 'Responsible for overall management and operations of Riverstone Resort and Spa, including guest services, staff management, and financial performance.'),
    (17, 'Corporate Director of Sales', 'Eskola LLC', 80000, 'Responsible for developing and implementing sales strategies, managing sales teams, and achieving revenue targets at Eskola LLC.'),
    (18, 'Recruiter', 'Edward Jones', 55000, 'Responsible for sourcing, recruiting, and hiring candidates to fill various positions within Edward Jones.'),
    (19, 'Corporate National Account Manager', 'Performance Food Service', 70000, 'Responsible for managing national accounts, building relationships with key clients, and driving sales growth at Performance Food Service.'),
    (20, 'Accounts Receivable Team Lead', 'Axle Logistics', 60000, 'Responsible for leading the accounts receivable team, managing collections, and resolving payment issues at Axle Logistics.'),
    (21, 'Logistics Consultant', 'Axle Logistics', 65000, 'Providing consulting services to clients on logistics and supply chain management, optimizing transportation networks, and reducing costs at Axle Logistics.'),
    (22, 'Director Supply Chain Management', 'TEAM Technologies', 75000, 'Responsible for overseeing supply chain operations, managing inventory, and optimizing distribution networks at TEAM Technologies.'),
    (23, 'Owner', 'What To Do Magazine', 100000, 'Responsible for overall management and direction of What To Do Magazine, including editorial content, advertising sales, and business development.'),
    (24, 'Staff Accountant', 'Purkey, Carter, Compton, Swann', 55000, 'Responsible for preparing financial statements, reconciling accounts, and analyzing financial data for clients at Purkey, Carter, Compton, Swann.'),
    (25, 'Accounts Receivable Clerk', 'Landair', 45000, 'Responsible for processing invoices, posting payments, and reconciling accounts receivable at Landair.'),
    (26, 'Accountant', 'Internet Marketing Expert Group', 55000, 'Responsible for managing financial records, preparing financial reports, and providing financial analysis for Internet Marketing Expert Group.');
;

-- Degree TABLE
INSERT INTO degree
	([type], major, concentration, person_id)
VALUES
	('AAS', 'Accounting', NULL, 1), 
	('AAS', 'Accounting', NULL, 2),
	('MA', 'Accounting', NULL, 3),
	('AAS', 'Accounting', NULL, 4),
	('BS', 'Accounting', 'Business Administration', 5),
	('BS', 'Marketing', 'Business Administration', 6),
	('BS', 'Supply Chain Management', 'Business Administration', 7),
	('MA', 'Accounting', NULL, 8),
	('AAS', 'Accounting', NULL, 9),
	('AS', 'Business', NULL, 10),
	('BS', 'Accounting', NULL, 11),
	('MA', 'Business', 'Business Administration', 12),
	('BS', 'Pharmacy', NULL, 13),
	('AS', 'Math', NULL, 14),
	('AS', 'Business', NULL, 15),
	('AS', 'Psychology and Criminal Investigation', NULL, 16),
	('BS', 'Business', 'Business Administration', 17),
	('BS', 'Business', 'Business Administration', 18),
	('AAS', 'Culinary', NULL, 19),
	('AAS', 'Business', 'Business Management', 20),
	('BS', 'Business', 'Supply Chain Management', 21),
	('BS', 'Business', 'Logistics', 22),
	('AAS', 'Hospitality Managemnt', NULL, 23),
	('AAS', 'Accounting', NULL, 24),
	('AAS', 'Accounting', NULL, 25),
	('AAS', 'Accounting', NULL, 26),
	('AAS', 'Accounting', NULL, 27)
;

-- Degree_Job TABLE
INSERT INTO degree_job_person
	(person_id, job_id, degree_id)
VALUES
	(1, 1, 1),
	(2, 2, 2),
	(3, 3, 3),
	(4, 4, 4),
	(5, 5, 5), 
	(6, 6, 6),
	(7, 7, 7),
	(8, 8, 8),
	(9, 9, 9),
	(10, 10, 10),
	(11, 11, 11), 
	(12, 12, 12), 
	(13, 13, 13),
	(14, 14, 14),
	(15, 15, 15), 
	(16, 16, 16),
	(17, 17, 17),
	(18, 18, 18),
	(19, 19, 19),
	(20, 20, 20),
	(21, 21, 21),
	(22, 22, 22),
	(23, 23, 23), 
	(24, 24, 24),
	(25, 25, 25),
	(26, 26, 26)
;