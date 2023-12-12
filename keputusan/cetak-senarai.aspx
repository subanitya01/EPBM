<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cetak-senarai.aspx.cs" Inherits="EPBM.keputusan.cetak_senarai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style>
		*,
		  *::before,
		  *::after {
			text-shadow: none !important;
			box-shadow: none !important;
		  }
		  a:not(.btn) {
			text-decoration: underline;
		  }
		  abbr[title]::after {
			content: " (" attr(title) ")";
		  }
		  pre {
			white-space: pre-wrap !important;
		  }
		  pre,
		  blockquote {
			border: 1px solid #adb5bd;
			page-break-inside: avoid;
		  }
		  tr,
		  img {
			page-break-inside: avoid;
		  }
		  p,
		  h2,
		  h3 {
			orphans: 3;
			widows: 3;
		  }
		  h2,
		  h3 {
			page-break-after: avoid;
		  }
		  @page {
			size: a3;
		  }
		  body {
			min-width: 992px !important;
		  }
		  .container {
			min-width: 992px !important;
		  }
		  .badge {
			border: 1px solid #000;
		  }
		  .table {
			border-collapse: collapse !important;
			border: 0;
		  }
		  .table td,
		  .table th {
			background-color: #fff !important;
		  }
		  .table-bordered th,
		  .table-bordered td {
			border: 1px solid #dee2e6 !important;
		  }
		  .table-dark {
			color: inherit;
		  }
		  .table-dark th,
		  .table-dark td,
		  .table-dark thead th,
		  .table-dark tbody + tbody {
			border-color: #dee2e6;
		  }
		.text-nowrap{ white-space: nowrap !important; }
		.btn-link{ display: inline-block; padding: 5px; }
		.table th,.table td{ padding: 5px; }
		.table ul{ padding-left: 20px; margin: 0; }
		.header{ display: flex; align-items: center;margin: 25px 0 0 0;}
		.header img{
			height: 50px;
			margin-right: 10px;
		}
		.filter{ 
			margin: 15px 5px 0 0;
			display:inline-block;
			border: 1px solid #808080;
			padding: 3px; 
		}
		header, footer{
			display: none;
		}
		form{
			margin-top: 5px;
		}
		table td.blank-cell{padding:15px;border:0 !important;}

		@media print {
			@page { size: landscape; }
			.p-hide { display:none; }
			header, footer{ display: block; }
			footer{ position: fixed;bottom: 0; right: 0; }
			header{ position: fixed;top: 0; left: 0; }
			thead {display: table-header-group; margin-top:10px;}
		}
	</style>
</head>
<body>
	<header>SULIT</header>
    <form id="form1" runat="server">
		<section>
			<a href="javascript:history.back()" class=" btn-link p-hide" >&lt- Kembali</a>
			<h3 class="header">
				<img src="/assets/img/icons/300px-Jata_MalaysiaV2.png" height="100" /> SENARAI KEPUTUSAN MESYUARAT EPBM
			</h3>
			<asp:Panel ID="PanelMesyuarat" runat="server" Visible="false" CssClass="filter">
				<span><b>Mesyuarat: </b> <asp:Literal ID="NamaMesyuarat" runat="server" /></span>
			</asp:Panel>
			<asp:Panel ID="PanelTajuk" runat="server" Visible="false" CssClass="filter">
				<span><b>Tajuk: </b> <asp:Literal ID="NamaTajuk" runat="server" /></span>
			</asp:Panel>
			<asp:Panel ID="PanelJabatan" runat="server" Visible="false" CssClass="filter">
				<span><b>Jabatan: </b> <asp:Literal ID="NamaBahagian" runat="server" /><asp:Literal ID="NamaJabatan" runat="server" /> </span>
			</asp:Panel>
			<asp:Panel ID="PanelStatus" runat="server" Visible="false" CssClass="filter">
				<span><b>Status: </b> <asp:Literal ID="NamaStatus" runat="server" /></span>
			</asp:Panel>
			<asp:Panel ID="PanelSyarikat" runat="server" Visible="false" CssClass="filter">
				<span><b>Syarikat Berjaya: </b> <asp:Literal ID="NamaSyarikat" runat="server" /></span>
			</asp:Panel>
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				ShowFooter="True"
				AutoGenerateColumns="False" 
				OnRowDataBound="GridView1_OnRowDataBound" 
				AllowPaging="False" 
				CssClass="table table-bordered table-striped table-hover">  

				<Columns>
					<asp:TemplateField HeaderText="BIL">
						<ItemTemplate><asp:Literal ID="Numbering" runat="server" /></ItemTemplate>
					</asp:TemplateField>

					<asp:BoundField DataField="MESYUARAT" HeaderText="MESYUARAT">
					</asp:BoundField>

					<asp:BoundField DataField="TAJUK" HeaderText="TAJUK">
					</asp:BoundField>

					<asp:BoundField DataField="JABATAN" HeaderText="JABATAN" HeaderStyle-CssClass="text-center">
					</asp:BoundField>

					<asp:BoundField DataField="MUKTAMAD" HeaderText="MUKTAMAD" HeaderStyle-CssClass="text-center" SortExpression="MUKTAMAD">
					</asp:BoundField>

					<asp:BoundField DataField="STATUS" HeaderText="STATUS" HeaderStyle-CssClass="text-center">
					</asp:BoundField>

					<asp:TemplateField HeaderText="KETERANGAN" HeaderStyle-CssClass="text-center">
						<ItemTemplate>
							<asp:Label ID="LblKeterangan" runat="server"><%# Eval("KETERANGAN").ToString().Replace(Environment.NewLine, "<br />") %></asp:Label>
						
							<asp:ListView runat="server"  ID="DetailsList">
								<LayoutTemplate>
									<ul class="list-group text-sm">
										<li id="itemPlaceholder" runat="server" />
									</ul>
								</LayoutTemplate>
								<ItemTemplate>
									<li class="list-group-item p-1 text-nowrap"><b><%#: Eval("Label") %>:</b> <br /><%#: Eval("Text") %></li>
								</ItemTemplate>
							</asp:ListView>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</section>
    </form>
	
	<footer>SULIT</footer>
	<script>window.print();</script>
</body>
</html>
