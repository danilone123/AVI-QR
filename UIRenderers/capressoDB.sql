USE [Capresso]
GO
/****** Object:  Table [dbo].[Renta]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Renta](
	[RentaId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FacturaNroInicial] [int] NOT NULL,
	[FechaLimiteEmision] [datetime] NOT NULL,
	[AutorizacionNro] [int] NOT NULL,
	[CodigoControl] [nvarchar](50) NOT NULL,
	[NITEmpresa] [nvarchar](50) NOT NULL,
	[RazonSocial] [nvarchar](50) NOT NULL,
	[Leyenda] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receta_Insumo]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receta_Insumo](
	[RecetaId] [numeric](18, 0) NOT NULL,
	[InsumoId] [numeric](18, 0) NOT NULL,
	[Cantidad] [float] NOT NULL,
	[Precio] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receta]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receta](
	[RecetaId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[CostoReceta] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto_Producto]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto_Producto](
	[ComboId] [numeric](18, 0) NOT NULL,
	[ProductoId] [numeric](18, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[ProductoId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](50) NULL,
	[Tipo] [nchar](10) NULL,
	[Tamano] [nvarchar](50) NULL,
	[PrecioVenta] [float] NOT NULL,
	[Imagen] [image] NULL,
	[CategoriaId] [numeric](18, 0) NOT NULL,
	[RecetaId] [numeric](18, 0) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificada] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido_Producto]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido_Producto](
	[PedidoId] [numeric](18, 0) NOT NULL,
	[ProductoId] [numeric](18, 0) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Total] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[PedidoId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Descuento] [nvarchar](50) NULL,
	[TipoPedido] [nvarchar](50) NOT NULL,
	[Pagado] [bit] NOT NULL,
	[FechaEntrega] [datetime] NOT NULL,
	[Entregado] [bit] NOT NULL,
	[ClienteId] [numeric](18, 0) NOT NULL,
	[TotalVenta] [float] NOT NULL,
	[FechaPedido] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Insumo]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Insumo](
	[InsumoId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Cantidad] [float] NOT NULL,
	[CostoUnidad] [float] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Presentacion] [nvarchar](50) NOT NULL,
	[Marca] [nvarchar](50) NOT NULL,
	[Unidad] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[FacturaId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[ClienteId] [numeric](18, 0) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FacturaNro] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[ComprasId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[FechaCompra] [datetime] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[CostoUnidad] [float] NOT NULL,
	[Total] [float] NOT NULL,
	[InsumoId] [numeric](18, 0) NOT NULL,
	[Descripcion] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Nombre] [nchar](30) NULL,
	[NIT] [nchar](30) NULL,
	[Telefono] [nchar](30) NULL,
	[Direccion] [nchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 08/09/2011 13:01:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[CategoriaId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[Tipo] [nvarchar](50) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
