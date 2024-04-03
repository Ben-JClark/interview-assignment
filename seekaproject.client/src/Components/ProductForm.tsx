import { useState } from 'react';
import { Dispatch, SetStateAction } from "react";
import type { Category } from "../App.tsx";
import type { FormData } from "../App.tsx";
import axios from "axios";

/**
 * Variables that manage the state of the form
 */
interface Props {
    formData: FormData; // Use state object that contains the data entered in the form
    fillFormData: Dispatch<SetStateAction<FormData>>; // Method to update useState hook formData
    clearForm: () => void; // Method to clear the form
    categories: Category[] | null; // Category options to display to the user
    createState: boolean; // If true the form should perform post operations, else the form will display delete and update operations
    getProducts: () => void; // Method to fetch all the products and update the Product table, will cause a page reload
}

/**
 * Display a Product form to the client that can perform POST, PUT, or DELETE operations
 */
function ProductForm({ formData, clearForm, categories, fillFormData, createState, getProducts }: Props) {
    const [errorMessage, setErrorMessage] = useState<string | null>(null);
    const [successMessage, setSuccessMessage] = useState<string | null>(null);

    return (
        <>
            <h3>{createState ? ("Create a Product") : ("Modify a Product")}</h3>

            {/* Name input */ }
            <label htmlFor="name_input">Name</label>
            <input required type="text" id="name_input" value={formData?.name || ''} onChange={(e) => fillFormData(prevState => ({ ...prevState, name: e.target.value }))} />

            {/* Category list input */}
            <label htmlFor="category_select">Category</label>
            <select id="category_select" value={formData?.categoryId /* Display the category the user has selected */} onChange={(e) => fillFormData(prevState => ({ ...prevState, categoryId: parseInt(e.target.value) }))}>
                {categories?.map(c => (
                    <option key={c.id} value={c.id}>{c.name}</option>
                ))}
            </select>

            {/* Price input */}
            <label htmlFor="price_input">Price</label>
            <input required type="number" id="price_input" value={formData?.price || 0} onChange={(e) => fillFormData(prevState => ({ ...prevState, price: parseFloat(e.target.value) }))} />

            {/* Description input */}
            <label htmlFor="description_input">Description</label>
            <textarea required id="description_input" rows={4} cols={50} value={formData?.description || ''} onChange={(e) => fillFormData(prevState => ({ ...prevState, description: e.target.value }))} />

            { /* if createState is true, show create button, else show Update and Delete button */}
            {createState ? (
                <button onClick={() => handleCreate()}>Create</button>
            ) : (
                <><button onClick={() => handleUpdate()}>Update</button>
                    <button onClick={() => handleDelete()}>Delete</button></>
            )}

            { /* show status messages */ }
            {successMessage ? (<p className="success_message">{successMessage}</p>) : null}
            {errorMessage ? (<p className="error_message">{errorMessage}</p>) : null}
        </>
    )

    async function handleCreate() {
        try {
            if (!validFormData()) {
                return;
            }

            await axios.post('http://localhost:5041/api/products', null, {
                params: {
                    Name: formData.name,
                    Description: formData.description,
                    Price: formData.price,
                    CategoryId: formData.categoryId
                }
            })
            successOccured(`Successfully created a ${formData.name} Product`);
        } catch (error) {
            console.log("Error creating a product: ", error);
            errorOccured(`Error creating a ${formData.name} product`);
        }
    }

    async function handleUpdate() {
        try {
            if (!validFormData()) {
                return;
            }

            await axios.put(`http://localhost:5041/api/products/${formData.id}`, null, {
                params: {
                    Name: formData.name,
                    Description: formData.description,
                    Price: formData.price,
                    CategoryId: formData.categoryId
                }
            });
            successOccured(`Successfully updated the ${formData.name} Product`);
        } catch (error) {
            console.log("Error updating a product: ", error);
            errorOccured(`Error updating the ${formData.name} product`);
        }
    }

    async function handleDelete() {
        try {
            await axios.delete(`http://localhost:5041/api/products/${formData.id}`);
            successOccured(`Successfully deleted the ${formData.name} Product`);
        } catch (error) {
            console.log("Error delete: ", error);
            errorOccured(`Error deleting the ${formData.name} product`);
        }
    }

    /**
     * Validate formData
     * @returns boolean result
     */
    function validFormData(): boolean {
        if (formData.name === "") {
            errorOccured("Product must have a Name");
            return false;
        }
        if (formData.price < 0) {
            errorOccured("Product Price must be greater than 0");
            return false;
        }
        if (formData.description === "") {
            errorOccured("Product must have a Description");
            return false;
        }
        return true;
    }

    /**
     * Reset the form and send a success message ot the client
     * @param message The success message
     */
    function successOccured(message: string) {
        clearForm();
        getProducts();
        setSuccessMessage(message);
        setErrorMessage(null);
    }

    /**
     * Display an error to the client
     * @param message The error message
     */
    function errorOccured(message: string) {
        setErrorMessage(message);
        setSuccessMessage(null);
    }
}

export default ProductForm;