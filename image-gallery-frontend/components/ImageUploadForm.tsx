"use client"

import React from 'react';
import {useFormState} from "react-dom"
import { createImage } from "@/actions/actions";
import {formState} from "@/lib/types";

const initialState : formState = {
    errors: {
        title: undefined,
        description: undefined
    }
}

const ImageUploadForm = () => {
    const [state, formAction] = useFormState(createImage, initialState)

    return (
        <form action={formAction}>
            <label className="form-control w-full max-w-xs mb-3">
                <div className="label">
                    <span className="label-text">Title</span>
                </div>
                <input name={"title"} type="text" placeholder="Image Title" className="input input-bordered rounded w-full max-w-xs" />
            <p aria-live="polite" className="sr-only">
                {state?.errors.title}
            </p>
            </label>
            <label className="form-control w-full max-w-xs mb-3">
                <div className="label">
                    <span className="label-text">Description</span>
                </div>
                <textarea name={"description"} className="textarea textarea-bordered h-24" placeholder="Image Description"></textarea>
            <p aria-live="polite" className="sr-only">
                {state?.errors.description}
            </p>
            </label>
            <label className="form-control w-full max-w-xs">
                <div className="label">
                    <span className="label-text">Pick an image</span>
                </div>
                <input name={"data"} type="file" className="file-input file-input-bordered w-full max-w-xs" />
            </label>
            <button type={"submit"} className={"btn mt-5"}>Submit</button>
        </form>
    );
};

export default ImageUploadForm;