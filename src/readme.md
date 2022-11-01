/* Create ára SQL server */
CREATE TABLE ACCOUNT_PLANS (
    id int IDENTITY(1,1) PRIMARY KEY,
    CodePlan varchar(20) NOT NULL,
    NamePlan varchar(100),
	TypePlan int,
    AcceptLauch bit,
	IsDeleted bit
);

Aplicado o conceito de Clean Arquiteture.
Testes unitários
