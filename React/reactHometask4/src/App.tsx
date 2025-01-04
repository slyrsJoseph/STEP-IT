import React, { useState } from "react";
import axios from "axios";
import { API_KEY, BASE_URL } from "./components/apiKey";
import { Searchbar } from "./components/searchbar";
import { ImageGallery } from "./components/imagegallery";
import { Loader } from "./components/loader";
import { Button } from "./components/button";
import { Modal } from "./components/modal";

const App: React.FC = () => {
  const [images, setImages] = useState([]);
  const [query, setQuery] = useState("");
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [imageUrl, setImageUrl] = useState("");

  const Images = async (searchQuery: string, currentPage: number) => {
    setLoading(true);
    try {
      const response = await axios.get(BASE_URL, {
        params: {
          q: searchQuery,
          page: currentPage,
          key: API_KEY,
          image_type: "photo",
          per_page: 12,
        },
      });

      setImages((prev) =>
        currentPage === 1
          ? response.data.hits.map((image) => ({
              id: image.id,
              webURL: image.webformatURL,
              imageURL: image.largeImageURL,
            }))
          : [
              ...prev,
              ...response.data.hits.map((image) => ({
                id: image.id,
                webURL: image.webformatURL,
                imageURL: image.largeImageURL,
              })),
            ]
      );
    } catch (error) {
      console.log("Error: ", error);
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = (searchQuery: string) => {
    setQuery(searchQuery);
    setPage(1);
    Images(searchQuery, 1);
  };

  const handleLoad = () => {
    const nextPage = page + 1;
    setPage(nextPage);
    Images(query, nextPage);
  };

  const handleImageClick = (url: string) => {
    setImageUrl(url);
    setShowModal(true);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setImageUrl("");
  };

  return (
    <div className="App">
      <Searchbar onSubmit={handleSearch} />
      {loading && <Loader />}
      <ImageGallery images={images} onImageClick={handleImageClick} />
      {images.length > 0 && !loading && <Button onClick={handleLoad} />}
      {showModal && <Modal imageUrl={imageUrl} onClose={handleCloseModal} />}
    </div>
  );
};

export default App;
