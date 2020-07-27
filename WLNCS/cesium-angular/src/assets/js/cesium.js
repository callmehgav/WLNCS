
function createModel(viewer ) {
  viewer.entities.removeAll();
  var url = "../assets/gltfModels/a10.glb";
  var position = Cesium.Cartesian3.fromDegrees(
    -84.065919,
    39.774654,
    5000.0
  );
  var heading = Cesium.Math.toRadians(135);
  var pitch = 1.6;//Sets the F16c right side up
  var roll = 0;
  var hpr = new Cesium.HeadingPitchRoll(heading, pitch, roll);
  var orientation = Cesium.Transforms.headingPitchRollQuaternion(
    position,
    hpr
  );
  addEntity(url,position,orientation,viewer);

}

function addEntity(url, position, orientation,viewer)
{
  
  var entity = viewer.entities.add({
    name: url,
    position: position,
    orientation: orientation,
    model: {
      uri: url,
      minimumPixelSize: 128,
      maximumScale: 128, //Controls the zoom out size of the craft
    },
  });
  viewer.trackedEntity = entity;
}

