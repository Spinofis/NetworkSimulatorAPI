USE [ComputerNetworkSimulator]
GO
/****** Object:  Table [dbo].[Pc]    Script Date: 22.01.2019 21:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pc](
	[id] [int] NOT NULL,
	[id_sim] [int] NULL,
	[node_number] [int] NULL,
	[pc_number] [int] NULL,
	[name] [varchar](50) NULL,
	[ip] [varchar](50) NULL,
	[mask] [varchar](50) NULL,
 CONSTRAINT [PK_Pc] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pc_Switch]    Script Date: 22.01.2019 21:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pc_Switch](
	[id] [int] NOT NULL,
	[id_pc] [int] NOT NULL,
	[id_switch] [int] NOT NULL,
 CONSTRAINT [PK_Pc_Switch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Router]    Script Date: 22.01.2019 21:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Router](
	[id] [int] NOT NULL,
	[id_sim] [int] NULL,
	[node_number] [int] NULL,
	[router_number] [int] NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_Router] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Router_interface]    Script Date: 22.01.2019 21:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Router_interface](
	[id] [int] NOT NULL,
	[id_router] [int] NOT NULL,
	[name] [varchar](50) NULL,
	[ip_host] [varchar](50) NULL,
	[ip_net] [varchar](50) NULL,
	[mask] [varchar](50) NULL,
 CONSTRAINT [PK_Router_Interface] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Router_switch]    Script Date: 22.01.2019 21:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Router_switch](
	[id] [int] NOT NULL,
	[id_router] [int] NOT NULL,
	[id_switch] [int] NOT NULL,
 CONSTRAINT [PK_Router_switch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Simulation]    Script Date: 22.01.2019 21:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Simulation](
	[id] [int] NOT NULL,
	[date_add] [datetime] NULL,
	[date_edit] [datetime] NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_Simulation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Switch]    Script Date: 22.01.2019 21:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Switch](
	[id] [int] NOT NULL,
	[id_sim] [int] NULL,
	[node_number] [int] NULL,
	[switch_number] [int] NULL,
	[name] [varchar](50) NULL,
 CONSTRAINT [PK_Switch] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pc]  WITH CHECK ADD  CONSTRAINT [FK_Pc_Simulation] FOREIGN KEY([id_sim])
REFERENCES [dbo].[Simulation] ([id])
GO
ALTER TABLE [dbo].[Pc] CHECK CONSTRAINT [FK_Pc_Simulation]
GO
ALTER TABLE [dbo].[Pc_Switch]  WITH CHECK ADD  CONSTRAINT [FK_Pc_Switch_Pc] FOREIGN KEY([id_pc])
REFERENCES [dbo].[Pc] ([id])
GO
ALTER TABLE [dbo].[Pc_Switch] CHECK CONSTRAINT [FK_Pc_Switch_Pc]
GO
ALTER TABLE [dbo].[Pc_Switch]  WITH CHECK ADD  CONSTRAINT [FK_Pc_Switch_Switch] FOREIGN KEY([id_switch])
REFERENCES [dbo].[Switch] ([id])
GO
ALTER TABLE [dbo].[Pc_Switch] CHECK CONSTRAINT [FK_Pc_Switch_Switch]
GO
ALTER TABLE [dbo].[Router]  WITH CHECK ADD  CONSTRAINT [FK_Router_Simulation] FOREIGN KEY([id_sim])
REFERENCES [dbo].[Simulation] ([id])
GO
ALTER TABLE [dbo].[Router] CHECK CONSTRAINT [FK_Router_Simulation]
GO
ALTER TABLE [dbo].[Router_interface]  WITH CHECK ADD  CONSTRAINT [FK_Router_interface_Router] FOREIGN KEY([id_router])
REFERENCES [dbo].[Router] ([id])
GO
ALTER TABLE [dbo].[Router_interface] CHECK CONSTRAINT [FK_Router_interface_Router]
GO
ALTER TABLE [dbo].[Router_switch]  WITH CHECK ADD  CONSTRAINT [FK_Router_switch_Router] FOREIGN KEY([id_router])
REFERENCES [dbo].[Router] ([id])
GO
ALTER TABLE [dbo].[Router_switch] CHECK CONSTRAINT [FK_Router_switch_Router]
GO
ALTER TABLE [dbo].[Router_switch]  WITH CHECK ADD  CONSTRAINT [FK_Router_switch_Switch] FOREIGN KEY([id_switch])
REFERENCES [dbo].[Switch] ([id])
GO
ALTER TABLE [dbo].[Router_switch] CHECK CONSTRAINT [FK_Router_switch_Switch]
GO
ALTER TABLE [dbo].[Switch]  WITH CHECK ADD  CONSTRAINT [FK_Switch_Simulation] FOREIGN KEY([id_sim])
REFERENCES [dbo].[Simulation] ([id])
GO
ALTER TABLE [dbo].[Switch] CHECK CONSTRAINT [FK_Switch_Simulation]
GO
