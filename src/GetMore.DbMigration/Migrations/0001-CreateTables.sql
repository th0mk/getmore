CREATE TABLE Employees(
    EmployeeId		INT                 NOT NULL IDENTITY(1,1),
    Name			VARCHAR (120)		NOT NULL,
    EmailAddress	VARCHAR (120)		NOT NULL,
    ContractedHours	INT					NOT NULL,

    CONSTRAINT PK_EmployeeId            PRIMARY KEY (EmployeeId)
);

CREATE TABLE Projects(
    ProjectId		INT                 NOT NULL IDENTITY(1,1),
    Name			VARCHAR (120)		NOT NULL,

    CONSTRAINT PK_Project_ProjectId     PRIMARY KEY (ProjectId)
);

CREATE TABLE UnitsOfWork(
    UnitOfWorkId	INT                 NOT NULL IDENTITY(1,1),
    ProjectId		INT					NOT NULL,
    EmployeeId		INT					NOT NULL,
    TimeAmount		TIME				NOT NULL,
    WorkDate		DATE				NOT NULL,

    CONSTRAINT PK_UnitOfWork_UnitOfWorkId  PRIMARY KEY (UnitOfWorkId),

    CONSTRAINT FK_UnitOfWork_ProjectId  FOREIGN KEY (ProjectId)     REFERENCES Projects (ProjectId),
    CONSTRAINT FK_UnitOfWork_EmployeeId FOREIGN KEY (EmployeeId)    REFERENCES Employees(EmployeeId)
);