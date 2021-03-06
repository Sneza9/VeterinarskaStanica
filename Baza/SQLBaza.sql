SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoviStruke](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tip] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TipoviStruke] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Veterinari](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](50) NOT NULL,
	[Prezime] [nvarchar](50) NOT NULL,
	[BrojTelefona] [nvarchar](15) NULL,
	[TipStrukeVeterinaraID] [int] NULL,
 CONSTRAINT [PK_Veterinari] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Veterinari_TipStrukeVeterinaraID] ON [dbo].[Veterinari]
(
	[TipStrukeVeterinaraID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Veterinari]  WITH CHECK ADD  CONSTRAINT [FK_Veterinari_TipoviStruke_TipStrukeVeterinaraID] FOREIGN KEY([TipStrukeVeterinaraID])
REFERENCES [dbo].[TipoviStruke] ([ID])
GO
ALTER TABLE [dbo].[Veterinari] CHECK CONSTRAINT [FK_Veterinari_TipoviStruke_TipStrukeVeterinaraID]
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VrsteZivotinja](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Vrsta] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_VrsteZivotinja] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zivotinje](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BrojKartona] [int] NOT NULL,
	[ImeZivotinje] [nvarchar](50) NOT NULL,
	[ImeVlasnika] [nvarchar](50) NOT NULL,
	[BrojTelefonaVlasnika] [nvarchar](15) NULL,
	[VrstaZivotinjeID] [int] NULL,
 CONSTRAINT [PK_Zivotinje] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Zivotinje_VrstaZivotinjeID] ON [dbo].[Zivotinje]
(
	[VrstaZivotinjeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Zivotinje]  WITH CHECK ADD  CONSTRAINT [FK_Zivotinje_VrsteZivotinja_VrstaZivotinjeID] FOREIGN KEY([VrstaZivotinjeID])
REFERENCES [dbo].[VrsteZivotinja] ([ID])
GO
ALTER TABLE [dbo].[Zivotinje] CHECK CONSTRAINT [FK_Zivotinje_VrsteZivotinja_VrstaZivotinjeID]
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZivotinjeVeterinari](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Lek] [nvarchar](50) NOT NULL,
	[PregledID] [int] NULL,
	[ZivotinjaID] [int] NULL,
	[VeterinarID] [int] NULL,
 CONSTRAINT [PK_ZivotinjeVeterinari] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ZivotinjeVeterinari_PregledID] ON [dbo].[ZivotinjeVeterinari]
(
	[PregledID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ZivotinjeVeterinari_VeterinarID] ON [dbo].[ZivotinjeVeterinari]
(
	[VeterinarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ZivotinjeVeterinari_ZivotinjaID] ON [dbo].[ZivotinjeVeterinari]
(
	[ZivotinjaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ZivotinjeVeterinari] ADD  DEFAULT (N'') FOR [Lek]
GO
ALTER TABLE [dbo].[ZivotinjeVeterinari]  WITH CHECK ADD  CONSTRAINT [FK_ZivotinjeVeterinari_Pregledi_PregledID] FOREIGN KEY([PregledID])
REFERENCES [dbo].[Pregledi] ([ID])
GO
ALTER TABLE [dbo].[ZivotinjeVeterinari] CHECK CONSTRAINT [FK_ZivotinjeVeterinari_Pregledi_PregledID]
GO
ALTER TABLE [dbo].[ZivotinjeVeterinari]  WITH CHECK ADD  CONSTRAINT [FK_ZivotinjeVeterinari_Veterinari_VeterinarID] FOREIGN KEY([VeterinarID])
REFERENCES [dbo].[Veterinari] ([ID])
GO
ALTER TABLE [dbo].[ZivotinjeVeterinari] CHECK CONSTRAINT [FK_ZivotinjeVeterinari_Veterinari_VeterinarID]
GO
ALTER TABLE [dbo].[ZivotinjeVeterinari]  WITH CHECK ADD  CONSTRAINT [FK_ZivotinjeVeterinari_Zivotinje_ZivotinjaID] FOREIGN KEY([ZivotinjaID])
REFERENCES [dbo].[Zivotinje] ([ID])
GO
ALTER TABLE [dbo].[ZivotinjeVeterinari] CHECK CONSTRAINT [FK_ZivotinjeVeterinari_Zivotinje_ZivotinjaID]
GO



