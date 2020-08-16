var app = angular.module("MissionManager", [])
    .controller("missionCtrl", function ($scope, $http , $interval) {

       // $scope.Missions = GetDataService.GetData();
        
        $scope.addMission = function (desc, pic) {
            var mission = { description: desc, picPath: pic };
            $http({
                method: "post",
                url: "http://localhost:51266/Main/InsertMission",
                datatype: "json",
                data: JSON.stringify(mission)
            }).then(function (response) {
               
            });

        };
            

        var var_1 = $interval(function () {
            $http.get("http://localhost:51266/Main/GetData").then(function (response) {
                $scope.Missions = response.data;
            });
            console.log("Updated!");
}, 5000);
        
       
       
    });


