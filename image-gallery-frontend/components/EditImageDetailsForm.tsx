"use client"

import React from 'react';
import {useFormState} from "react-dom"
import { updateImage } from "@/actions/actions";
import {formState} from "@/lib/types";

const initialState : formState = {
    errors: {
        title: undefined,
        description: undefined
    }
}

type GalleryImage = {
    id: string,
    title: string,
    image_url: string,
    description: string,
    date_modified: Date,
    created_on: Date
}

const EditImageDetailsForm = ({image}: { image: GalleryImage }) => {
    const [state, formAction] = useFormState(updateImage, initialState)

    return (
        <form action={formAction}>
            <input name="id" value={image.id} hidden={true} />
            <label className="form-control w-full max-w-xs mb-3">
                <div className="label">
                    <span className="label-text">Title</span>
                </div>
                <input name={"title"} defaultValue={image.title} type="text" placeholder="Image Title" className="input input-bordered rounded w-full max-w-xs" />
                <p aria-live="polite" className="sr-only">
                    {state?.errors.title}
                </p>
            </label>
            <label className="form-control w-full max-w-xs mb-3">
                <div className="label">
                    <span className="label-text">Description</span>
                </div>
                <textarea name={"description"} defaultValue={image.description} className="textarea textarea-bordered h-24" placeholder="Image Description"></textarea>
                <p aria-live="polite" className="sr-only">
                    {state?.errors.description}
                </p>
            </label>
            <button type={"submit"} className={"btn mt-5"}>Submit</button>
        </form>
    );
};

export default EditImageDetailsForm;