import React from 'react';
import EditImageDetailsForm from "@/components/EditImageDetailsForm";
import httpCommon from "@/lib/http-common";

const EditImageDetails = async ({params}: {params: {id: string}}) => {
    const image : ApiResponse = await httpCommon.get(`/api/images/${params.id}`)
        .then(res => res.data)
    return (
        <div>
            <h1>Edit image info here</h1>
            <EditImageDetailsForm image={image.data} />
        </div>
    );
};

export default EditImageDetails;

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