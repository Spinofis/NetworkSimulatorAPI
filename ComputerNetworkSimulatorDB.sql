USE [ComputerNetworkSimulator]
GO
/****** Object:  Table [dbo].[Links]    Script Date: 27.01.2019 21:18:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Links](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSim] [int] NULL,
	[NodeNumber1] [varchar](50) NULL,
	[NodeNumber2] [varchar](50) NULL,
 CONSTRAINT [PK_Links] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pc]    Script Date: 27.01.2019 21:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pc](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_sim] [int] NULL,
	[node_number] [varchar](50) NULL,
	[pc_number] [int] NULL,
	[name] [varchar](50) NULL,
	[hostIdentity] [varchar](50) NULL,
 CONSTRAINT [PK_Pc] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Router]    Script Date: 27.01.2019 21:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Router](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_sim] [int] NULL,
	[node_number] [varchar](50) NULL,
	[router_number] [int] NULL,
	[name] [varchar](50) NULL,
	[hostIdentity] [varchar](50) NULL,
 CONSTRAINT [PK_Router] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Simulation]    Script Date: 27.01.2019 21:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Simulation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date_add] [datetime] NULL,
	[date_edit] [datetime] NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_Simulation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Switch]    Script Date: 27.01.2019 21:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Switch](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_sim] [int] NULL,
	[node_number] [varchar](50) NULL,
	[switch_number] [int] NULL,
	[name] [varchar](50) NULL,
	[hostIdentity] [varchar](50) NULL,
 CONSTRAINT [PK_Switch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Links]  WITH CHECK ADD  CONSTRAINT [FK_Links_Simulation] FOREIGN KEY([IdSim])
REFERENCES [dbo].[Simulation] ([id])
GO
ALTER TABLE [dbo].[Links] CHECK CONSTRAINT [FK_Links_Simulation]
GO
ALTER TABLE [dbo].[Pc]  WITH CHECK ADD  CONSTRAINT [FK_Pc_Simulation] FOREIGN KEY([id_sim])
REFERENCES [dbo].[Simulation] ([id])
GO
ALTER TABLE [dbo].[Pc] CHECK CONSTRAINT [FK_Pc_Simulation]
GO
ALTER TABLE [dbo].[Router]  WITH CHECK ADD  CONSTRAINT [FK_Router_Simulation] FOREIGN KEY([id_sim])
REFERENCES [dbo].[Simulation] ([id])
GO
ALTER TABLE [dbo].[Router] CHECK CONSTRAINT [FK_Router_Simulation]
GO
ALTER TABLE [dbo].[Switch]  WITH CHECK ADD  CONSTRAINT [FK_Switch_Simulation] FOREIGN KEY([id_sim])
REFERENCES [dbo].[Simulation] ([id])
GO
ALTER TABLE [dbo].[Switch] CHECK CONSTRAINT [FK_Switch_Simulation]
GO
