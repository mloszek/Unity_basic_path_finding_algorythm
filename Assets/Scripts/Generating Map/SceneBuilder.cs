﻿using System;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{
    public Obstacle[] obstacles;
    public GameObject field;
    public GameObject obstacle;
    public GameObject start;
    public GameObject end;

    private GameObject m_camera;
    private ClearMapGenerator clearMap;
    private LoadedMapGenerator loadedMap;
    private float distanceBetweenFields = 1.1f;

    void Start()
    {
        m_camera = GameObject.FindGameObjectWithTag("MainCamera");       
        
        GenerateMap();
        SetCamera(m_camera.GetComponent<Camera>());
    }

    void GenerateMap()
    {
        if (!MapProperties.isLoaded)
        {
            clearMap = new ClearMapGenerator(MapProperties.height, MapProperties.width, MapProperties.difficulty);
            clearMap.CreateMap(field);
            clearMap.CreateObstacles(obstacle);
            clearMap.CreateStartAndFinish(start, end);
        }
        else
        {
            SaveLoadScript.Load();
            loadedMap = new LoadedMapGenerator(MapProperties.height, MapProperties.width);
            loadedMap.CreateMap(field);
            loadedMap.CreateObstacles(obstacle);
            loadedMap.CreateStartAndFinish(start, end);
        }
    }

    void SetCamera(Camera m_camera)
    {
        m_camera.transform.position = new Vector3((MapProperties.width - 1) * distanceBetweenFields / 2, 1,
            (MapProperties.height - 1) * distanceBetweenFields / 2);

        if (MapProperties.width > MapProperties.height)
        {
            m_camera.orthographicSize = MapProperties.width * distanceBetweenFields / 2;
        }
        else
        {
            m_camera.orthographicSize = MapProperties.height * distanceBetweenFields / 2;
        }
    }
}
