<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai.aspx.cs" Inherits="EPBM.keputusan.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">SENARAI <strong>KEPUTUSAN</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="input-group">
				<select class="form-select">
					<option>SEMUA KOLUM</option>
					<option>MESYUARAT</option>
					<option>TAJUK</option>
					<option>JABATAN</option>
					<option>STATUS</option>
					<option>SYARIKAT BERJAYA</option>
				</select>
				<input type="text" class="form-control" placeholder="Carian...">
				<button class="btn btn-primary rounded-end" type="button"><i class="align-middle" data-feather="search"></i></button>
				<a class="btn btn-secondary ms-3 rounded" data-bs-toggle="collapse" href="#advancedSearch" role="button" aria-expanded="false" aria-controls="advancedSearch"><i class="align-middle" data-feather="filter"></i></a>
			</div>
		</div>
	</div>
	<div id="advancedSearch" class="card collapse mt-3">
		<div class="card-header pb-0">
			<h5 class="card-title">CARIAN LANJUT</h5>
		</div>
		<div class="row card-body">
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">MESYUARAT</label>
					<select class="form-select">
					  <option selected>SILA PILIH</option>
						<option>JKSH BIL. 1/2023</option>

					</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">TAJUK</label>
					<input type="text" class="form-control" placeholder="">
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">JABATAN</label>
					<select class="form-select">
					  <option selected>SILA PILIH</option>
						<option value="Pegawai Kementerian">Kementerian Sumber Asli, Alam Sekitar dan Perubahan Iklim</option>
						<option value="JUPEM">Jabatan Ukur dan Pemetaan Malaysia</option>
						<option value="JMG">Jabatan Mineral dan Geosains Malaysia</option>
						<option value="JPSM">Jabatan Perhutanan Semenanjung Malaysia</option>
						<option value="PERHILITAN">Jabatan Perlindungan Hidupan Liar dan Taman Negara</option>
						<option value="FRIM">Insitut Penyelidikan Perhutanan Malaysia</option>
						<option value="INSTUN">Institut Tanah dan Ukur Negara</option>
						<option value="JKPTG">Jabatan Ketua Pengarah Tanah dan Galian</option>
						<option value="NAHRIM">Institut Penyelidikan Hidraulik Kebangsaan Malaysia</option>
						<option value="JBK">Jabatan Biokeselamatan</option>
						<option value="JPS">Jabatan Pengairan dan Saliran</option>
						<option value="JPP">Jabatan Perkhidmatan Pembetungan</option>
						<option value="JAS">Jabatan Alam Sekitar</option>
						<option value="MET">Jabatan Meteorologi Malaysia</option>

					</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">STATUS</label>
					<select class="form-select">
					  <option>SILA PILIH</option>
					  <option>SELESAI</option>
					  <option>PERINGKAT MOF</option>
					  <option>IKLAN SEMULA</option>
					  <option>BATAL</option>
					</select>
				</div>
				<div class="col-12 col-lg-6 mb-3">
					<label class="control-label">SYARIKAT BERJAYA</label>
					<div class="input-group">
						<select class="form-select">
						  <option>MENGANDUNGI</option>
						  <option>SAMA DENGAN</option>
						  <option>BERMULA DENGAN</option>
						  <option>BERAKHIR DENGAN</option>
						</select>
						<input type="text" class="form-control w-50" placeholder="">
					</div>
				</div>
				<div class="col-12 text-end">
					<button type="submit" class="btn btn-primary">CARI</button>
				</div>
		</div>
	</div>
	<div class="card">
		<div class="card-body table-responsive">
			<div class="row input-group-sm justify-content-between">
				<div class="col-sm-6 col-md-5 mb-3">
					<label>SUSUNAN BERDASARKAN: <asp:Label ID="lblSortRecord" runat="server" /></label>
				</div>
				<div class="col-sm-3 col-md-2 mb-3 text-end">
					
				</div>
			</div>
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				AutoGenerateColumns="False" 
				OnRowDataBound="GridView1_OnRowDataBound" 
				OnSorting="GridView1_Sorting" 
				OnPageIndexChanging="GridView1_PageIndexChanging" 
				AllowSorting="true"
				AllowCustomPaging="true"
				AllowPaging="True" 
				PageSize="20"
				CssClass="table table-bordered table-striped table-hover">  

				<PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="5" FirstPageText="&laquo;" LastPageText="&raquo;" />
				<PagerStyle HorizontalAlign = "Right" CssClass = "bs-pager" />

				<Columns>
					<asp:TemplateField HeaderText="#">
						<ItemTemplate><asp:Literal ID="Numbering" runat="server" /></ItemTemplate>
					</asp:TemplateField>

					<asp:BoundField DataField="MESYUARAT" HeaderText="MESYUARAT" SortExpression="MESYUARAT">
					</asp:BoundField>

					<asp:BoundField DataField="TAJUK" HeaderText="TAJUK" SortExpression="TAJUK">
					</asp:BoundField>

					<asp:BoundField DataField="JABATAN" HeaderText="JABATAN" HeaderStyle-CssClass="text-center" SortExpression="JABATAN">
					</asp:BoundField>

					<asp:TemplateField HeaderText="STATUS" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" SortExpression="STATUS">
						<ItemTemplate>
							<asp:Label ID="lblStatus" CssClass="badge" runat="server"><%# Eval("STATUS") %></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>

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
									<li class="list-group-item p-1"><b><%#: Eval("Label") %>:</b> <%#: Eval("Text") %></li>
								</ItemTemplate>
							</asp:ListView>
						</ItemTemplate>
					</asp:TemplateField>

					<asp:TemplateField ItemStyle-CssClass="text-center">
						<ItemTemplate>
							<a href="/keputusan/papar.aspx?id=<%# Eval("Id") %>&ReturnURL=<%# System.Web.HttpUtility.UrlEncode("/keputusan/senarai.aspx") %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
