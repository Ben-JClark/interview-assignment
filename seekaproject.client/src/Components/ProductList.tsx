import { Dispatch, SetStateAction } from "react";
import type { Product } from "../App.tsx";
import type { FormData } from "../App.tsx";

/**
 * Variables that manage the state of the ProductForm and ProductList
 */
interface Props {
    clearForm: () => void; // Method to clear the ProductForm
    setCreateState: Dispatch<SetStateAction<boolean>>; // method to set the ProductForm to create state
    products: Product[] | null; // Products to display in the table
    fillFormData: Dispatch<SetStateAction<FormData>>; // method to fill the ProductForm with data from the product selected by the user
}

/**
 * Display a clickable table containing all the products
 */
function ProductList({ products, fillFormData, clearForm, setCreateState }: Props) {

    return (
        <>
            <h3 id="tabelLabel">Products</h3>
            <p>To select a product, click a table row</p>
            {products === null ? (<em>Loading...Please refresh once the ASP.NET backend has started.</em>) :
                (
                    <>
                        <table className="table table-striped" aria-labelledby="tabelLabel">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Category</th>
                                    <th>Description</th>
                                    <th>Price</th>
                                </tr>
                            </thead>
                            <tbody>
                                {products ? products.map(p =>
                                    /* When a user clicks a row, fill in the Product form with the details of the product selected, then the user can perform PUT or DELETE operations */
                                    <tr className="product_listing" key={p.id} onClick={() => { fillFormData({ id: p.id, name: p.name, description: p.description, price: p.price, categoryId: p.categoryId }); setCreateState(false) }}>
                                        <td>{p.name}</td>
                                        <td>{p.categoryName}</td>
                                        <td>{p.description}</td>
                                        <td>{p.price}</td>
                                    </tr>
                                ): null}
                            </tbody>
                        </table>
                        {/* Clear the Product form and set it to the Create state*/}
                        <button onClick={() => { clearForm(); setCreateState(true) }}>Create</button>
                    </>
                )}

        </>
    );

}

export default ProductList;