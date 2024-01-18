'use server'

import { z } from 'zod'
import {formState} from "@/lib/types";
import http from "@/lib/http-common";
import {redirect} from "next/navigation";

const createImageSchema = z.object({
    title: z.string({
        invalid_type_error: 'Invalid Title'
    }).min(2),
    description: z.string().min(5)
})

export async function createImage(prevState: any, formData: FormData) {
    const validatedFields = createImageSchema.safeParse({
        title: formData.get("title"),
        description: formData.get("description")
    });

    let completed;

    if (!validatedFields.success){
        return {
            errors: validatedFields.error.flatten().fieldErrors
        }
    }

    await http.post("/api/images", formData, {
        headers: {
            "Content-Type": "multipart/form-data",
        }
    }).then(response => {
        if (response.data.succeeded){
            completed = true
        } else {
            return {
                errors: response.data.error_message
            }
        }
    })
        .catch(error => console.log(error))

    if (completed) redirect("/")
}

export async function updateImage(prevState: any, formData: FormData) {
    const validatedFields = createImageSchema.safeParse({
        title: formData.get("title"),
        description: formData.get("description")
    });

    let completed;

    if (!validatedFields.success){
        return {
            errors: validatedFields.error.flatten().fieldErrors
        }
    }

    const body = {
        title: formData.get("title"),
        description: formData.get("description")
    }

    await http.put(`/api/images/${formData.get("id")}`, body, {
        headers: {
            "Content-Type": "application/json",
        }
    }).then(response => {
        if (response.data.succeeded){
            completed = true;
        } else {
            return {
                errors: response.data.error_message
            }
        }
    })
        .catch(error => console.log(error))

    if (completed) redirect("/")
}