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

		@media print {
			.p-hide { display:none; }
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
			<a href="javascript:history.back()" class=" btn-link p-hide" >&lt- Kembali</a>
			<h3>SENARAI KEPUTUSAN MESYUARAT EPBM</h3>
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
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
									<li class="list-group-item p-1 text-nowrap"><b><%#: Eval("Label") %>:</b> <%#: Eval("Text") %></li>
								</ItemTemplate>
							</asp:ListView>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
        </div>
    </form>
	<script>window.print();</script>
</body>
</html>
