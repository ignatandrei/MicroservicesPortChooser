CREATE TABLE [dbo].[MSPC_Register](
	[Name] [nvarchar](150) NULL,
	[Hostname] [nvarchar](150) NULL,
	[Port] [int] NULL,
	[Tag] [nvarchar](150) NULL,
	[Authority] [nvarchar](50) NULL,
	[PCName] [nvarchar](150) NULL,
	[stringRegisteredDate] [text] NULL,
	[EnvData] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


