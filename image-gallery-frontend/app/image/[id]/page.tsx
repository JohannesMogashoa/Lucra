import React from 'react';
import httpCommon from "@/lib/http-common";
import Image from "next/image";
import Link from "next/link";
import DeleteImageButton from "@/components/DeleteImageButton";

const ImageDetail = async ({params}: {params: {id: string}}) => {
    const image : ApiResponse = await httpCommon.get(`/api/images/${params.id}`)
        .then(res => res.data)
    return (
        <div>
            <Link href={`/image/edit/${params.id}`} className={"btn"}>Edit</Link>
            <DeleteImageButton id={params.id} />
            <Image height={250} width={250} src={image.data.image_url} alt={image.data.title} />
            <h1>{image.data.title}</h1>
            <p>{image.data.description}</p>
        </div>
    );
};

export default ImageDetail;

type ApiResponse = {
    data: Image,
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