<!DOCTYPE html>
<html lang="en">

<head>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
	
	<link rel="preconnect" href="https://fonts.gstatic.com">
	<link rel="shortcut icon" href="img/icons/icon-48x48.png" />

	<title>ePBM</title>

	<link href="css/app.css" rel="stylesheet">
	<link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;600&display=swap" rel="stylesheet">
</head>

<body>
	<div class="wrapper overflow-hidden">
		<nav id="sidebar" class="sidebar js-sidebar">
			<div class="sidebar-content js-simplebar">
				<a class="sidebar-brand" href="dashboard.php">
				  <span class="align-middle">ePBM</span>
				</a>

				<ul class="sidebar-nav">

					<li class="sidebar-item <?= $active=="dashboard" ? "active" : "" ?>">
						<a class="sidebar-link" href="dashboard.php">
						  <i class="align-middle" data-feather="sliders"></i> <span class="align-middle">Utama</span>
						</a>
					<li class="sidebar-header">
						Permohonan
					</li>
					</li>

					<li class="sidebar-item <?= $active=="p-daftar-permohonan" ? "active" : "" ?>">
						<a class="sidebar-link" href="p-daftar-permohonan.php">
						  <i class="align-middle" data-feather="file-plus"></i> <span class="align-middle">Daftar Permohonan</span>
						</a>
					</li>

					<li class="sidebar-item <?= $active=="p-senarai-permohonan" ? "active" : "" ?>">
						<a class="sidebar-link" href="p-senarai-permohonan.php">
						  <i class="align-middle" data-feather="file-text"></i> <span class="align-middle">Senarai Permohonan</span>
						</a>
					</li>

					<li class="sidebar-item <?= $active=="p-senarai-pengesahan" ? "active" : "" ?>">
						<a class="sidebar-link" href="p-senarai-pengesahan.php">
						  <i class="align-middle" data-feather="check-square"></i> <span class="align-middle">Senarai Pengesahan</span>
						</a>
					</li>
					
					<li class="sidebar-header">
						Mesyuarat
					</li>

					<li class="sidebar-item <?= $active=="m-daftar-mesyuarat" ? "active" : "" ?>">
						<a class="sidebar-link" href="m-daftar-mesyuarat.php">
						  <i class="align-middle" data-feather="folder-plus"></i> <span class="align-middle">Daftar Mesyuarat</span>
						</a>
					</li>

					<li class="sidebar-item <?= $active=="m-senarai-mesyuarat" ? "active" : "" ?>">
						<a class="sidebar-link" href="m-senarai-mesyuarat.php">
						  <i class="align-middle" data-feather="folder"></i> <span class="align-middle">Keputusan Mesyuarat</span>
						</a>
					</li>

					<li class="sidebar-header">
						Keputusan
					</li>
					<li class="sidebar-item <?= $active=="k-senarai-keputusan" ? "active" : "" ?>">
						<a class="sidebar-link" href="k-senarai-keputusan.php">
						  <i class="align-middle" data-feather="inbox"></i> <span class="align-middle">Senarai Keputusan</span>
						</a>
					</li>
					<li class="sidebar-header">
						Laporan
					</li>

					<li class="sidebar-item <?= $active=="l-carian-laporan" ? "active" : "" ?>">
						<a class="sidebar-link" href="l-carian-laporan.php">
						  <i class="align-middle" data-feather="bar-chart-2"></i> <span class="align-middle">Carian Laporan</span>
						</a>
					</li>
			</div>
		</nav>

		<div class="main">
			<nav class="navbar navbar-expand navbar-light navbar-bg">
				<a class="sidebar-toggle js-sidebar-toggle">
          <i class="hamburger align-self-center"></i>
        </a>

				<div class="navbar-collapse collapse">
					<ul class="navbar-nav navbar-align">
						</li>
						<li class="nav-item dropdown">
							<a class="nav-icon dropdown-toggle d-inline-block d-sm-none" href="#" data-bs-toggle="dropdown">
                <i class="align-middle" data-feather="settings"></i>
              </a>

							<a class="nav-link dropdown-toggle d-none d-sm-inline-block" href="#" data-bs-toggle="dropdown">
                <img src="img/avatars/avatar.jpg" class="avatar img-fluid rounded me-1" alt="Charles Hall" /> <span class="text-dark">Charles Hall</span>
              </a>
							<div class="dropdown-menu dropdown-menu-end">
								<a class="dropdown-item" href="profil.php"><i class="align-middle me-1" data-feather="user"></i> Profile</a>
								<a class="dropdown-item" href="index.php">Log out</a>
							</div>
						</li>
					</ul>
				</div>
			</nav>

			<main class="content">
				<div class="container-fluid p-0">