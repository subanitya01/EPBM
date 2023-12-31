﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="EPBM.auth.login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<title>ePBM</title>
	<link rel="preconnect" href="https://fonts.gstatic.com">
	<link rel="shortcut icon" href="~/assets/img/icons/300px-Jata_MalaysiaV2.png" />
	<link href="~/assets/css/app.css" rel="stylesheet">
	<link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600&display=swap" rel="stylesheet">
</head>

<body class="bg-animated-light-x bg-secondary bg-opacity-10">
	<main class="d-flex w-100">
		<div class="container d-flex flex-column">
			<div class="row vh-100">
				<div class="col-sm-10 col-md-8 col-lg-6 col-xl-5 mx-auto d-table h-100">
					<div class="d-table-cell">

						<div class="text-center mt-4">
                            <div class="row justify-content-center">
                                <div class="col-3">
							        <img class="img-fluid" src="/assets/img/icons/300px-Jata_MalaysiaV2.png" alt="" />
                                </div>
                            </div>
							<h1 class="h2 fw-bold mb-0">ePBM</h1>
							<p class="lead fw-bold">
								Sistem Pihak Berkuasa Melulus NRECC
							</p>
						</div>
                        <div class="card">
                            <div class="card-body">
                                <div class="">
                                    <form id="account" method="post" defaultfocus="txtUsername" defaultbutton="BtnLogin" runat="server">
                                        <div asp-validation-summary="ModelOnly" class="text-danger" role="alert"></div>
                                        <div class="form-floating">
                                            <asp:TextBox ID="txtUsername" runat="server" class="form-control" required="required" placeholder="No Kad Pengenalan"></asp:TextBox>
                                            <label for="txtUsername" class="form-label">No. Kad Pengenalan</label>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsername"
                                                CssClass="text-danger" ErrorMessage="No. Kad Pengenalan wajib diisi." />
                                        </div>
                                        <div class="form-floating">
                                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control" required="required" placeholder="Kata Laluan"></asp:TextBox>
                                            <label for="txtPassword" class="form-label">Kata Laluan</label>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                                                CssClass="text-danger" ErrorMessage="Kata laluan wajib diisi." />
                                        </div>
                                        <div class="">
                                            <div class="form-check align-items-center">
                                                <label for="chkPersist" class="form-label">
                                                    <asp:CheckBox ID="chkPersist" runat="server" class="form-check-input" />
                                                    Ingat Saya
                                                </label>
                                            </div>
                                        </div>
                                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                            <span class="text-danger">
                                                <asp:Literal runat="server" ID="errorLabel" />
                                            </span>
                                        </asp:PlaceHolder>
                                        <div class="d-grid gap-2 mt-3">
                                            <asp:LinkButton ID="BtnLogin" runat="server" CssClass="btn btn-lg btn-primary" OnClick="Login_Click">Log masuk</asp:LinkButton>
                                        </div>
                                        <div class="text-center mt-2">
                                                <a id="forgot-password" href="https://profile.nrecc.gov.my/ForgotPassword.aspx" target="_blank">Lupa kata laluan?</a>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
						<p class="lead text-center fw-bold">
							Kementerian Sumber Asli, Alam Sekitar <br />dan Perubahan Iklim
						</p>
					</div>
				</div>
			</div>
		</div>
	</main>
	<div class='light x1'></div>
	<div class='light x2'></div>
	<div class='light x3'></div>
	<div class='light x4'></div>
	<div class='light x5'></div>
	<div class='light x6'></div>
	<div class='light x7'></div>
	<div class='light x8'></div>
	<div class='light x9'></div>

</body>
</html>