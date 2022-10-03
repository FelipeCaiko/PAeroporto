create database OnTheFly;
use OnTheFly;

create table Passageiro (
	CPF varchar(11) not null,
	Nome varchar(50) not null,
	DataNascimento date not null,
	Sexo varchar(1) not null,
	UltimaCompra datetime not null,
	DataCadastro datetime not null,
	Situacao char(1) not null,
	constraint PK_Passageiro PRIMARY KEY (CPF)
);

create table CompanhiaAerea (
	CNPJ varchar(14) not null,
	RazaoSocial varchar(50) not null,
	DataAbertura date not null,
	DataCadastro datetime not null,
	UltimoVoo datetime not null,
	Situacao varchar(1) not null,
	constraint PK_CompanhiaAerea PRIMARY KEY (CNPJ)
);

create table Aeronave (
	Inscricao varchar(6) not null,
	CNPJCompanhia varchar(14) not null,
	Capacidade int not null,
	UltimaVenda datetime not null,
	DataCadastro datetime not null,
	Situacao varchar(1) not null,
	CONSTRAINT PK_Aeronave PRIMARY KEY (Inscricao),
	CONSTRAINT FK_Companhia FOREIGN KEY (CNPJCompanhia) references CompanhiaAerea(CNPJ)
);

create table Cadastro_Restritos (
	CPF varchar(11) not null,
	constraint PK_Restritos PRIMARY KEY (CPF)
);

create table Cadastro_Bloqueados (
	CNPJ varchar(14) not null,
	constraint PK_Bloqueados PRIMARY KEY (CNPJ)
);

create table Iatas (
	Sigla varchar(3) not null,
	Descricao varchar(150) not null,
	constraint PK_Iatas PRIMARY KEY (Sigla)
);


create table Voo (
	ID varchar(5) not null,
	InscAeronave varchar(6) not null,
	DataCadastro datetime not null,
	DataVoo datetime not null,
	Destino varchar(3) not null,
	AssentosOcupados int,
	Situacao char(1) not null,
	CONSTRAINT PK_Voo PRIMARY KEY (ID),
	CONSTRAINT FK_Aeronave FOREIGN KEY (InscAeronave) references Aeronave(Inscricao),
	CONSTRAINT Pk_Destino FOREIGN KEY (Destino) REFERENCES Iatas(Sigla)
);

create table PassagemVoo(
	ID varchar(6) not null,
	IDVoo varchar(5) not null,
	DataUltimaOperacao datetime not null,
	Valor float not null,
	Situacao char(1) not null,
	CONSTRAINT PK_Passagem PRIMARY KEY (ID, IDVoo),
	CONSTRAINT FK_IDVoo FOREIGN KEY (IDVoo) references Voo(ID)
);

create table Venda (
	ID int identity not null,
	DataVenda datetime not null,
	ValorTotal float,
	CPFPassageiro varchar(11) not null,
	CONSTRAINT PK_Venda PRIMARY KEY (ID),
	CONSTRAINT FK_CPFPassageiro FOREIGN KEY (CPFPassageiro) references Passageiro(CPF)
);

create table ItemVenda (
	ID int identity not null,
	IDVenda int not null,
	ValorUnit float not null,
	CONSTRAINT PK_ItemVenda PRIMARY KEY (ID),
	CONSTRAINT FK_IDVenda FOREIGN KEY (IDVenda) references Venda(ID)
);

insert into Iatas 
	values ('BSB', 'Aeroporto Internacional de Brasília / Presidente Juscelino Kubitschek'),
	('CGH', 'Aeroporto Internacional de São Paulo / Congonhas'),
	('GIG', 'Aeroporto Internacional do Rio de Janeiro / Galeão-Antônio Carlos Jobim'),
	('SSA', 'Aeroporto Internacional de Salvador / Deputado Luis Eduardo Magalhães'),
	('FLN', 'Aeroporto Internacional de Florianópolis / Hercílio Luz'),
	('POA', 'Aeroporto Internacional de Porto Alegre / Salgado Filho'),
	('VCP', 'Aeroporto Internacional de Viracopos / Campinas'),
	('REC', 'Aeroporto Internacional do Recife/ Guararapes – Gilberto Freyre'),
	('CWB', 'Aeroporto Internacional de Curitiba / Afonso Pena'),
	('BEL', 'Aeroporto Internacional de Belém / Val de Cans'),
	('VIX', 'Aeroporto de Vitória – Eurico de Aguiar Salles'),
	('SDU', 'Aeroporto Santos Dumont'),
	('CGB', 'Aeroporto Internacional de Cuiabá / Marechal Rondon'),
	('CGR','Aeroporto Internacional de Campo Grande'),
	('FOR','Aeroporto Internacional de Fortaleza / Pinto Martins'),
	('MCP','Aeroporto Internacional de Macapá'),
	('MGF','Aeroporto Regional de Maringá / Silvio Name Junior'),
	('GYN','Aeroporto de Goiânia / Santa Genoveva'),
	('NVT','Aeroporto Internacional de Navegantes / Ministro Victor Konder'),
	('MAO','Aeroporto Internacional de Manaus / Eduardo Gomes'),
	('NAT','Aeroporto Internacional de Natal / Augusto Severo'),
	('BPS','Aeroporto Internacional de Porto Seguro'),
	('MCZ','Aeroporto de Maceió / Zumbi dos Palmares'),
	('GRU','Aeroporto Internacional de São Paulo/Guarulhos-Governador André Franco Motoro');

CREATE PROCEDURE CadastroPassagens (@valor float, @idVoo varchar(5))
AS 
    BEGIN
	declare 
	@idPassagem int = 0,
	@count int = 0,
	@situacao varchar(1)= 'L', 
	@qtd int,
	@dataUltimaOperacao DateTime = GetDate()

	SELECT @qtd = Capacidade FROM Aeronave, Voo WHERE Inscricao = InscAeronave AND ID = @idVoo
	
	WHILE @count < @qtd
		BEGIN
			SET @count = @count + 1
			SET @idPassagem = @idPassagem + 1

	        INSERT INTO PassagemVoo (ID, IDVoo, DataUltimaOperacao, Valor, Situacao) VALUES (@idPassagem, @idVoo, @dataUltimaOperacao, @valor, @situacao)
		END
END
