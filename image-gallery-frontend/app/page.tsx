export default async function Home() {
	const response = await fetch("https://localhost:7210/api/images");

	console.log(await response.json());

	return (
		<main>
			<div className="navbar bg-base-100">
				<div className="navbar-start">
					<a className="btn btn-ghost text-xl">Lucra Image Gallery</a>
				</div>
				<div className="navbar-end">
					<a className="btn">Login</a>
				</div>
			</div>
			<h1>Welcome to Lucra Image Gallery</h1>
			<img src="https://localhost:7210/protected/gallery/634d86c9-2b1a-4cbd-b81f-c4b16b3e5500_king.jpg" />
		</main>
	);
}
