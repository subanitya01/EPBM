<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="senarai.aspx.cs" Inherits="EPBM.Permohonan.senarai" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

<link href="../assets/css/gridview.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<h1 class="h3 mb-3">SENARAI <strong>PERMOHONAN</strong></h1>
	<div class="card">
		<div class="card-body">
			<div class="input-group">
				<select class="form-select">
					<option>SEMUA KOLUM</option>
					<option>TAJUK</option>
					<option>JABATAN</option>
					<option>STATUS</option>
				</select>
				<input type="text" class="form-control w-25" placeholder="Carian...">
				<button class="btn btn-primary" type="button"><i class="align-middle" data-feather="search"></i></button>
			</div>
		</div>
	</div>
	<div class="card">
		<div class="card-body table-responsive">
	<%--		<table class="table table-bordered table-striped table-hover">--%>

				<asp:GridView ID="Senarai" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="Senarai_PageIndexChanging" CssClass="table table-striped table-bordered table-hover Grid" AllowPaging="True">
                                        <PagerSettings Mode="Numeric" Position="Bottom" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TajukPerolehan" HeaderText="Tajuk" SortExpression="Tajuk">
                                                <ItemStyle Width="25%" Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NamaJabatan" HeaderText="Jabatan" SortExpression="Jabatan">
                                                <ItemStyle Width="25%" Wrap="true" />
                                            </asp:BoundField>
											<asp:BoundField DataField="NamaPencipta" HeaderText="NamaPencipta" SortExpression="NamaPencipta">
                                                <ItemStyle Width="25%" Wrap="true" />
                                            </asp:BoundField>
											<asp:BoundField DataField="NamaBahagian" HeaderText="Bahagian" SortExpression="NamaBahagian">
                                                <ItemStyle Width="25%" Wrap="true" />
                                            </asp:BoundField>
                                       
                                        </Columns>
                                    </asp:GridView>



		<%--	</table>--%>
			<nav aria-label="Page navigation example">
			<%--  <ul class="pagination justify-content-end">
				<li class="page-item">
				  <a class="page-link" href="#" aria-label="Previous">
					<span aria-hidden="true">&laquo;</span>
				  </a>
				</li>
				<li class="page-item active"><a class="page-link" href="#">1</a></li>
				<li class="page-item"><a class="page-link" href="#">2</a></li>
				<li class="page-item"><a class="page-link" href="#">3</a></li>
				<li class="page-item">
				  <a class="page-link" href="#" aria-label="Next">
					<span aria-hidden="true">&raquo;</span>
				  </a>
				</li>
			  </ul>--%>
			</nav>
		</div>
	</div>
	<div class="modal fade" id="deleteModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-danger">
					<h5 class="modal-title text-white text-truncate">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="mb-0">Anda pasti untuk menghapuskan permohonan ini?</p>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-danger" data-bs-dismiss="modal">Hapus</button>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="meetingModal" tabindex="-1" aria-modal="true" role="dialog">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header bg-info">
					<h5 class="modal-title text-white text-truncate">PEROLEHAN PERKHIDMATAN SEWAAN PERALATAN ICT BAGI KEMENTERIAN TENAGA DAN SUMBER ASLI (KETSA) TAHUN 2022 - 2025</td>
				  <td>JABATAN UKUR DAN PEMETAAN MALAYSIA</h5>
					<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
				</div>
				<div class="modal-body m-3">
					<p class="">Anda pasti untuk bawa permohonan ini ke mesyuarat?</p>
					
					<div>
						<select class="form-select mb-3" required>
						  <option>SILA PILIH MESYUARAT</option>
						  <option>MLP BIL. 1/2023</option>
						  <option>JKSH BIL. 1/2023</option>
						</select>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Batal</button>
					<button type="button" class="btn btn-info" data-bs-dismiss="modal">Teruskan</button>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
