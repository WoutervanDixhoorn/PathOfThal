namespace PathOfThal
{
    public class MapLayer
    {
        
        Terrain terrain;

        public MapLayer(Terrain iTerrain){
            terrain = iTerrain;
        }

        public Terrain GetTerrain(){
            return terrain;
        }

        public void SetTerrain(Terrain iTerrain){
            terrain = iTerrain;
        }

    }
}