import React from "react";
import { ImageGalleryItem } from "./imagegalleryitem";

export const ImageGallery: React.FC<{
  images: { id: number; webURL: string; ImageURL: string }[];
  onImageClick: (url: string) => void;
}> = ({ images, onImageClick }) => {
  return (
    <ul className="gallery">
      {images.map((image) => (
        <ImageGalleryItem
          key={image.id}
          image={{
            id: image.id,
            webUrl: image.webURL,
            imageUrl: image.ImageURL,
          }}
          onClick={onImageClick}
        />
      ))}
    </ul>
  );
};
