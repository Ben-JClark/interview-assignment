import { useEffect, useState } from 'react';
import axios from "axios";
import ProductList from "./Components/ProductList";
import ProductForm from "./Components/ProductForm";
import './App.css';

export type Product = {
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
    categoryName: string;
};

export type FormData = {
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
};

export type Category = {
    id: number;
    name: string;
};

function App() {
    // List of all the products to be displayed on a table
    const [products, setProducts] = useState<Product[] | null>(null);
    // List of all the categories
    const [categories, setCategories] = useState<Category[] | null>(null);
    // Data entered from the ProductList
    const [formData, fillFormData] = useState<FormData>({ id: -1, name: "", description: "", price: 0, categoryId: 1 });
    // Hook that manages the state of ProductForm, if true, only POST operations can be performed, else PUT and DELETE
    const [createState, setCreateState] = useState<boolean>(true);

    /**
     * Startup method calls
     */
    useEffect(() => {
        getProducts();
        getCategories();
    }, []);


    return (
        <div className="container">
            <div className="component">
                <ProductList clearForm={clearForm} products={products} fillFormData={fillFormData} setCreateState={setCreateState} />
            </div>
            <div className="component">
                <ProductForm getProducts={getProducts } clearForm={clearForm} categories={categories} formData={formData} fillFormData={fillFormData} createState={createState} />
            </div>
        </div>
    );

    /**
     * Clear ProductForm and change its state to POST
     * This should be called after a successful DELETE or PUT request
     */
    function clearForm() {
        fillFormData({ id: -1, name: "", description: "", price: 0, categoryId: 1 });
        setCreateState(true);
    }

    /**
     * Fetch all the Products 
     */
    async function getProducts() {
        try {
            const response = await axios.get('http://localhost:5041/api/products');
            const data: Product[] = response.data;
            setProducts(data);
        } catch (error) {
            console.log(error);
        }
    }

    /**
     * Fetch all the categories 
     */
    async function getCategories() {
        try {
            const response = await axios.get('http://localhost:5041/api/categories');
            const data: Category[] = response.data;
            setCategories(data);
        } catch (error) {
            console.log(error);
        }
    }
}

export default App;