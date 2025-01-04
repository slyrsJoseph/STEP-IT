import React from "react";

export const ImageGalleryItem: React.FC<{
  image: { id: number; webUrl: string; imageUrl: string };
  onClick: (url: string) => void;
}> = ({ image, onClick }) => {
  return (
    <li className="gallery-item" onClick={() => onClick(image.imageUrl)}>
      <img src={image.webUrl} alt="" />
    </li>
  );
};
