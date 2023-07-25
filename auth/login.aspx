<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="EPBM.login" %>

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

<body class="bg-animated-light">
	<main class="d-flex w-100">
		<div class="container d-flex flex-column">
			<div class="row vh-100">
				<div class="col-sm-10 col-md-8 col-lg-6 col-xl-5 mx-auto d-table h-100">
					<div class="d-table-cell align-middle">

						<div class="text-center mt-4">
							<img class="img-fluid w-25" src="/assets/img/icons/300px-Jata_MalaysiaV2.png" alt="" />
							<h1 class="h2 text-white">Selamat Datang!</h1>
							<p class="lead text-white">
								Log masuk untuk tindakan seterusnya
							</p>
						</div>
                        <div class="card">
                            <div class="card-body">
                                <div class="m-sm-3">
                                    <form id="account" method="post" runat="server">
                                        <div asp-validation-summary="ModelOnly" class="text-danger" role="alert"></div>
                                        <div class="form-floating mb-3">
                                            <input asp-for="Input.Email" class="form-control" autocomplete="username" aria-required="true" placeholder="name@example.com" autofocus />
                                            <label asp-for="Input.Email" class="form-label">Nama Pengguna</label>
                                            <span asp-validation-for="Input.Email" class="text-danger"></span>
                                        </div>
                                        <div class="form-floating mb-3">
                                            <input asp-for="Input.Password" class="form-control" autocomplete="current-password" aria-required="true" placeholder="password" />
                                            <label asp-for="Input.Password" class="form-label">Kata Laluan</label>
                                            <span asp-validation-for="Input.Password" class="text-danger"></span>
                                        </div>
                                        <div class="mb-3">
                                            <div class="form-check align-items-center">
                                                <label asp-for="Input.RememberMe" class="form-label">
                                                    <input class="form-check-input" asp-for="Input.RememberMe" />
                                                    Ingat Saya
                                                </label>
                                            </div>
                                        </div>
                                        <div class="d-grid gap-2 mt-3">
                                            <a href="/index.aspx" id="login-submit" type="submit" class="btn btn-lg btn-primary">Log masuk</a>
                                        </div>
                                        <div class="text-center mt-2">
                                            <p>
                                                <a id="forgot-password" href="https://profile.nrecc.gov.my/ForgotPassword.aspx" target="_blank">Lupa kata laluan?</a>
                                            </p>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
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