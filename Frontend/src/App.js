import logo from './logo.svg';
import './App.css';
import axios from 'axios'
import { useEffect, useState } from 'react';

function App() {



  const [Resturant, setRestaurant] = useState([]);
  const [distance,setDistance]= useState(10);
  const [latitude,setLatidude]=useState(0); 
  const [longitude,setLongitude]=useState(0); 

const Search = () => {

function Coordinat(position)
{
setLatidude(position.coords.latitude);
setLongitude(position.coords.longitude);
 
}

  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(Coordinat);
  } else {
    alert("Lokasyon bilgisi browserınızda desteklenmiyor");
  }



  axios
  .get(`/api/Restaurant?latitude=${latitude}&longitude=${longitude}&maxdistance=${distance}&count=5`)
  .then((response) => {
    setRestaurant(response.data);
  });

console.log(Resturant)

}



  return (
<div className='main_page'>
  <div className='header_text'><span class="material-symbols-outlined">local_pizza</span>InfoSet Pizza</div>
  <div className='restaurant_list'>
    <div className='restaurant_list_head'>
    <label>Yakınlık seviyesi</label>
    <div>
      {distance} (KM)
      <br></br>
    <input type="range" min="1" max="10" onChange={(e) => {
      setDistance(e.target.value)
    }}/>
    </div>
    <button type="button" onClick={() => Search()}>En yakın şubeleri bul</button>
    </div>
    {Resturant.map((item )=> {
      return (
        <div className='restaurant_item'>
        <p>{item.name}</p>
        </div>
      )
    })}
  </div>
</div>
  );
}

export default App;
