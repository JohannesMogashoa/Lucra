import Link from "next/link";
import http from "@/lib/http-common"
import Image from "next/image";


export default async function Home() {
	const response : ApiResponse = await http.get("/api/images").then(response => response.data)

	console.log(response)

	return (
		<main>
			<div className="navbar bg-base-100">
				<div className="navbar-start">
					<a className="btn btn-ghost text-xl">Lucra Image Gallery</a>
				</div>
				<div className="navbar-end gap-3">
					<Link href={"/upload"} className="btn">Upload</Link>
					<a className="btn">Login</a>
				</div>
			</div>
			{response.data.length ? response.data.map(i => (
				<Link className={"mb-5"} key={i.id} href={`/image/${i.id}`}>
					<Image width={250} height={250} src={i.image_url}  alt={i.title} />
				</Link>
			)) : (
				<p className={"text-center"}>There are currently no images uploaded...</p>
			)}
		</main>
	);
}

type ApiResponse = {
	data: Image[],
	succeeded: boolean,
	errors: string[],
	error_message: string
}

type Image = {
	id: string,
	title: string,
	image_url: string,
	description: string,
	date_modified: Date,
	created_on: Date
}