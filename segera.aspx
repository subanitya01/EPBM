<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="segera.aspx.cs" Inherits="EPBM.segera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<link href="../assets/css/gridview.css" rel="stylesheet" />


<style>
img {
padding-top: 10px;
width: 22px;
height: 27px;
}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
	
	<h1 class="h3 mb-3 d-flex">
		<span class="text-truncate w-100">SENARAI PEROLEHAN UNTUK <strong>TINDAKAN SEGERA</strong></span>
		<span class="btn-group btn-group-sm float-end" role="group">
			<asp:HyperLink ID="HyperLink3" NavigateUrl="/" runat="server" CssClass="btn btn-secondary text-nowrap" ><i class="align-middle" data-feather="corner-up-left"></i> Kembali</asp:HyperLink>
		</span>
	</h1>
	<ul class="nav nav-tabs" id="myTab" role="tablist">
	  <li class="nav-item" role="presentation">
		<button class="nav-link active" id="valid-tab" data-bs-toggle="tab" data-bs-target="#valid-tab-pane" type="button" role="tab" aria-controls="valid-tab-pane" aria-selected="true">Dalam Tempoh Sahlaku</button>
	  </li>
	  <li class="nav-item" role="presentation">
		<button class="nav-link" id="invalid-tab" data-bs-toggle="tab" data-bs-target="#invalid-tab-pane" type="button" role="tab" aria-controls="invalid-tab-pane" aria-selected="false">Tamat Tempoh Sahlaku</button>
	  </li>
	</ul>
	<div class="tab-content p-3 border border-top-0" id="myTabContent">
		<div class="tab-pane fade show active" id="valid-tab-pane" role="tabpanel" aria-labelledby="valid-tab" tabindex="0">
			<div class="card">
				<div class="card-header pb-0">
					<h5 class="card-title">SENARAI PEROLEHAN YANG BELUM DIBAWA KE MESYUARAT</h5>
				</div>
				<div class="card-body table-responsive">
					<asp:GridView ID="Senarai" runat="server" AutoGenerateColumns="False" EmptyDataText="Tiada Rekod Dijumpai." ShowHeaderWhenEmpty="True" CssClass="table table-striped table-bordered table-hover Grid" OnRowDataBound="Senarai_DataBound">
						<Columns>
							<asp:TemplateField HeaderText="ID" Visible="false" SortExpression="ID">
								<ItemTemplate>
									<asp:Label ID="lblID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="No.">
								<ItemStyle Width="0.5%" Wrap="true" />
								<ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="TAJUK" SortExpression="Tajuk">
								<ItemStyle Width="50%" Wrap="true" />
								<ItemTemplate>
									<asp:Label ID="lblTajukUtama" runat="server" Text='<%# Eval("Tajuk") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="NamaJabatan" HeaderText="KEMENTERIAN /JABATAN" SortExpression="NamaJabatan">
								<ItemStyle Width="20%" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="ShortName" Visible="false">
								<ItemStyle Width="0.5%" Wrap="true" />
								<ItemTemplate>
									<asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("ShortName") %>' />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="Harga" HeaderText="HARGA" DataFormatString="RM {0:n}" SortExpression="Harga">
								<ItemStyle Width="12%" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="STATUS PERMOHONAN" Visible="true" SortExpression="Status_Permohonan">
								<ItemStyle Width="0.5%" Wrap="true" />
								<ItemTemplate>
									<asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status_Permohonan") %>'></asp:Label>
									<asp:Label ID="lblIDStatus" Visible="false" runat="server" Text='<%# Eval("IdStatusPermohonan") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="TINDAKAN">
								<ItemStyle Width="9%" Wrap="true" />
								<ItemTemplate>
									<asp:HyperLink ID="HyperLinkPapar" runat="server" NavigateUrl='<%# Eval("Id", "~/permohonan/papar.aspx?ID={0}") %>' title="Papar"><i class="align-middle" data-feather="eye"></i></asp:HyperLink>
									<asp:HyperLink ID="HyperLinkEdit" runat="server" Visible="false" NavigateUrl='<%# Eval("Id", "~/permohonan/edit.aspx?ID={0}") %>' title="Kemaskini"><i class="align-middle" data-feather="edit-2"></i></asp:HyperLink>

								</ItemTemplate>
							</asp:TemplateField>
						</Columns>

					</asp:GridView>
				</div>
			</div>
			<div class="card">
				<div class="card-header pb-0">
					<h5 class="card-title">SENARAI PEROLEHAN YANG DIPUTUSKAN DI MOF</h5>
				</div>
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
								<ItemTemplate><asp:Literal ID="Numbering" runat="server" /></ItemTemplate>
							</asp:TemplateField>

							<asp:BoundField DataField="MESYUARAT" HeaderText="MESYUARAT" SortExpression="MESYUARAT">
							</asp:BoundField>

							<asp:BoundField DataField="TAJUK" HeaderText="TAJUK" SortExpression="TAJUK">
							</asp:BoundField>

							<asp:BoundField DataField="JABATAN" HeaderText="JABATAN" HeaderStyle-CssClass="text-center" SortExpression="JABATAN">
							</asp:BoundField>

							<asp:BoundField DataField="MUKTAMAD" HeaderText="MUKTAMAD" HeaderStyle-CssClass="text-center" SortExpression="MUKTAMAD">
							</asp:BoundField>

							<asp:TemplateField HeaderText="STATUS KEPUTUSAN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" SortExpression="STATUS">
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
									<a href="/keputusan/papar.aspx?id=<%# Eval("Id") %>&ReturnURL=<%# System.Web.HttpUtility.UrlEncode("/segera.aspx") %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
									<asp:HyperLink ID="LinkEditMOF" runat="server" CssClass="text-secondary" title="Keputusan MOF" ><i class="align-middle" data-feather="edit"></i></asp:HyperLink>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
				</div>
			</div>
		</div>
		<div class="tab-pane fade" id="invalid-tab-pane" role="tabpanel" aria-labelledby="invalid-tab" tabindex="0">
			<div class="card">
				<div class="card-header pb-0">
					<h5 class="card-title">SENARAI PEROLEHAN YANG BELUM DIBAWA KE MESYUARAT</h5>
				</div>
				<div class="card-body table-responsive">
					<asp:GridView ID="Senarai2" runat="server" AutoGenerateColumns="False" EmptyDataText="Tiada Rekod Dijumpai." ShowHeaderWhenEmpty="True" CssClass="table table-striped table-bordered table-hover Grid" OnRowDataBound="Senarai_DataBound">
						<Columns>
							<asp:TemplateField HeaderText="ID" Visible="false" SortExpression="ID">
								<ItemTemplate>
									<asp:Label ID="lblID" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="No.">
								<ItemStyle Width="0.5%" Wrap="true" />
								<ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="TAJUK" SortExpression="Tajuk">
								<ItemStyle Width="50%" Wrap="true" />
								<ItemTemplate>
									<asp:Label ID="lblTajukUtama" runat="server" Text='<%# Eval("Tajuk") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="NamaJabatan" HeaderText="KEMENTERIAN /JABATAN" SortExpression="NamaJabatan">
								<ItemStyle Width="20%" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="ShortName" Visible="false">
								<ItemStyle Width="0.5%" Wrap="true" />
								<ItemTemplate>
									<asp:HiddenField ID="hfStatus" runat="server" Value='<%#Eval("ShortName") %>' />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="Harga" HeaderText="HARGA" DataFormatString="RM {0:n}" SortExpression="Harga">
								<ItemStyle Width="12%" Wrap="true" />
							</asp:BoundField>
							<asp:TemplateField HeaderText="STATUS PERMOHONAN" Visible="true" SortExpression="Status_Permohonan">
								<ItemStyle Width="0.5%" Wrap="true" />
								<ItemTemplate>
									<asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status_Permohonan") %>'></asp:Label>
									<asp:Label ID="lblIDStatus" Visible="false" runat="server" Text='<%# Eval("IdStatusPermohonan") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="TINDAKAN">
								<ItemStyle Width="9%" Wrap="true" />
								<ItemTemplate>
									<asp:HyperLink ID="HyperLinkPapar" runat="server" NavigateUrl='<%# Eval("Id", "~/permohonan/papar.aspx?ID={0}") %>' title="Papar"><i class="align-middle" data-feather="eye"></i></asp:HyperLink>
									<asp:HyperLink ID="HyperLinkEdit" runat="server" Visible="false" NavigateUrl='<%# Eval("Id", "~/permohonan/edit.aspx?ID={0}") %>' title="Kemaskini"><i class="align-middle" data-feather="edit-2"></i></asp:HyperLink>

								</ItemTemplate>
							</asp:TemplateField>
						</Columns>

					</asp:GridView>
				</div>
			</div>
			<div class="card">
				<div class="card-header pb-0">
					<h5 class="card-title">SENARAI PEROLEHAN YANG DIPUTUSKAN DI MOF</h5>
				</div>
				<div class="card-body table-responsive">
					<asp:GridView 
						ID="GridView2" 
						runat="server" 
						EmptyDataText="Tiada Rekod Dijumpai." 
						ShowHeaderWhenEmpty="True" 
						AutoGenerateColumns="False" 
						OnRowDataBound="GridView1_OnRowDataBound" 
						CssClass="table table-bordered table-striped table-hover">  

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

							<asp:BoundField DataField="MUKTAMAD" HeaderText="MUKTAMAD" HeaderStyle-CssClass="text-center" SortExpression="MUKTAMAD">
							</asp:BoundField>

							<asp:TemplateField HeaderText="STATUS KEPUTUSAN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" SortExpression="STATUS">
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
									<a href="/keputusan/papar.aspx?id=<%# Eval("Id") %>&ReturnURL=<%# System.Web.HttpUtility.UrlEncode("/segera.aspx") %>" title="Papar"><i class="align-middle" data-feather="eye"></i></a>
									<asp:HyperLink ID="LinkEditMOF" runat="server" CssClass="text-secondary" title="Keputusan MOF" ><i class="align-middle" data-feather="edit"></i></asp:HyperLink>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
				</div>
			</div>
		</div>
	</div>
	<asp:TextBox ID="ID" runat="server" Style="display: none;" type="text"></asp:TextBox>		


</asp:Content>
