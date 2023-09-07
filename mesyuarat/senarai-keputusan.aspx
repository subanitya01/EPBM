<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai-keputusan.aspx.cs" Inherits="EPBM.mesyuarat.senarai_keputusan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	<h1 class="h3 mb-3">SENARAI KEPUTUSAN BAGI <strong><asp:Literal ID="TajukMesyuarat" runat="server"></asp:Literal></strong></h1>
	<div class="card">
		<div class="card-body table-responsive">
			<asp:GridView 
				ID="GridView1" 
				runat="server" 
				EmptyDataText="Tiada Rekod Dijumpai." 
				ShowHeaderWhenEmpty="True" 
				AutoGenerateColumns="False" 
				OnRowDataBound="GridView1_OnRowDataBound" 
				CssClass="table table-bordered table-striped table-hover">  
            <Columns>
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="TAJUK" HeaderText="TAJUK">
                </asp:BoundField>

                <asp:BoundField DataField="JABATAN" HeaderText="JABATAN" HeaderStyle-CssClass="text-center">
                </asp:BoundField>

				<asp:TemplateField HeaderText="STATUS" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
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
								<li class="list-group-item p-1"><!--%#: Eval("Role") %--></li>
							</ItemTemplate>
						</asp:ListView>
					</ItemTemplate>
				</asp:TemplateField>

                <asp:TemplateField ItemStyle-CssClass="text-center">
                    <ItemTemplate>
						<a href="/keputusan/papar.aspx?id=<%# Eval("Id") %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/keputusan/edit.aspx?id=<%# Eval("Id") %>" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
						
					</ItemTemplate>
                </asp:TemplateField>
            </Columns>
			</asp:GridView>
			<table class="table table-bordered table-striped table-hover">
			  <thead>
				<tr>
				  <th scope="col">#</th>
				  <th scope="col">TAJUK</th>
				  <th scope="col">JABATAN</th>
				  <th scope="col">STATUS</th>
				  <th scope="col">KETERANGAN</th>
				  <th scope="col"></th>
				</tr>
			  </thead>
			  <tbody>
				<tr>
				  <th scope="row">1</th>
				  <td class="">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td class="text-center"></td>
				  <td></td>
				  <td></td>
				  <td class="table-action">
						<a href="/keputusan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/keputusan/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
					</td>
				</tr>
				<tr>
				  <th scope="row">2</th>
				  <td class="">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</td>
				  <td class="text-center"><span class="badge text-bg-info">PERINGKAT MOF</span></td>
				  <td>
					<ul class="list-group text-sm">
					  <li class="list-group-item p-1"><b>SYARIKAT BERJAYA:</b> BINTARA SOLUTIONS SDN BHD</li>
					  <li class="list-group-item p-1"><b>NILAI:</b> RM 3,456,789.00</li>
					  <li class="list-group-item p-1"><b>TEMPOH:</b> 2 BULAN</li>
					</ul>
				  </td>
				  <td class="table-action">
						<a href="/keputusan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/keputusan/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
					</td>
				</tr>
				<tr>
				  <th scope="row">3</th>
				  <td class="">PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN ALAM SEKITAR DAN AIR TAHUN 2020 HINGGA 2023</td>
				  <td>BAHAGIAN PENGURUSAN MAKLUMAT</td>
				  <td class="text-center"><span class="badge text-bg-success">SELESAI</span></td>
				  <td>
					<ul class="list-group text-sm">
					  <li class="list-group-item p-1"><b>SYARIKAT BERJAYA:</b> HIJRAH INOVATIF SDN. BHD.</li>
					  <li class="list-group-item p-1"><b>NILAI:</b> RM 3,456,789.00</li>
					  <li class="list-group-item p-1"><b>TEMPOH:</b> 2 BULAN</li>
					</ul>
				  </td>
				  <td class="table-action">
						<a href="/keputusan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/keputusan/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
					</td>
				</tr>
				<tr>
				  <th scope="row">4</th>
				  <td class="">PEROLEHAN PEMBAHARUAN LANGGANAN APLIKASI ZOOM BAGI VIRTUAL MEETING KEMENTERIAN SUMBER ASLI, ALAM SEKITAR DAN PERUBAHAN IKLIM</td>
				  <td>BAHAGIAN PENGURUSAN MAKLUMAT</td>
				  <td class="text-center"><span class="badge text-bg-danger">BATAL</span></td>
				  <td>MAKLUMAT TIDAK SEPADAN</td>
				  <td class="table-action">
						<a href="/keputusan/papar.aspx" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
						<a href="/keputusan/edit.aspx" class="text-secondary" title="Edit"><i class="align-middle" data-feather="edit-2"></i></a>
					</td>
				</tr>
			  </tbody>
			</table>
		</div>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
