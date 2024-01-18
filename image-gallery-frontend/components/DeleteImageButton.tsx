"use client"

import React from 'react';
import {deleteImage} from "@/actions/actions";

const DeleteImageButton = ({id}: {id: string}) => {
    return (
        <button className={"btn"} onClick={async () => await deleteImage(id)}>Delete</button>
    );
};

export default DeleteImageButton;