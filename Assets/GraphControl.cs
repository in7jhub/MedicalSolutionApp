using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChartAndGraph
{
    public class GraphControl : MonoBehaviour
    {
        public bool isBezier;

        float timer = 0;
        float unit = 0.05f;
        float prevTime = 0;
        int unitCnt = 0;

        GraphChart graph;
        Vector2 prevPoint;
        float yInput = 0;
        List<Vector2> recordedPoint = new List<Vector2>();

        void Start()
        {
            graph = gameObject.GetComponent<GraphChart>();
            graph.DataSource.ClearCategory("Player1");
        }

        void OnEnable()
        {
            graph = gameObject.GetComponent<GraphChart>();
            graph.DataSource.ClearCategory("Player1");
        }

        void Update()
        {
            timer += Time.deltaTime;

            makeInput();

            if(isBezier)
            {
                graph.DataSource.StartBatch(); // start a new update batch
                graph.DataSource.ClearCategory("Player1");
                graph.DataSource.SetCurveInitialPoint("Player1", 0, 0, -1);
                recordAndRenderBezier();
                drawCurrentBezierPoint();          
            }
            else
            {
                if(timer - prevTime > unit)
                {
                    if(unit < 0.2f)
                    {
                        unit += 0.0001f;
                    }
                    else
                    {
                        unit = 0.2f;
                    }

                    Debug.Log(unit);

                    graph.DataSource.StartBatch();
                    recordedPoint.Add(new Vector2(unitCnt * unit, yInput));
                    graph.DataSource.AddPointToCategory(
                        "Player1",
                        recordedPoint[recordedPoint.Count - 1].x,
                        recordedPoint[recordedPoint.Count - 1].y,
                        -1f
                    );
                    graph.DataSource.EndBatch(); // end the update batch . this call willrender the graph
                    unitCnt++;
                    prevTime = timer;
                }
            }
        }

        void drawCurrentBezierPoint()
        {
            if (recordedPoint.Count == 0)
            {
                graph.DataSource.AddCurveToCategory(
                    "Player1",
                    new DoubleVector2(timer * 0.4f, 0),
                    new DoubleVector2(timer - timer * 0.4f, yInput),
                    new DoubleVector2(timer, yInput),
                    -1f
                );
            }
            else
            {
                Vector2 lastRecordedPoint = recordedPoint[recordedPoint.Count - 1];
                double sub = 0; //(timer - lastRecordedPoint.x) * 0.4f;
                
                graph.DataSource.AddCurveToCategory(
                    "Player1",
                    new DoubleVector2(lastRecordedPoint.x + sub, lastRecordedPoint.y),
                    new DoubleVector2(timer - sub, yInput),
                    new DoubleVector2(timer, yInput),
                    -1f
                );
            }
        }

        void recordAndRenderBezier()
        {
            for (int i = 0; i < (int)timer; i++)
            {
                if (recordedPoint.Count == i)
                {
                    recordedPoint.Add(new Vector2(i + 1f, yInput));
                    break;
                }

                if (i == 0)
                {
                    prevPoint = Vector2.zero;
                }
                else
                {
                    prevPoint = recordedPoint[i - 1];
                }

                graph.DataSource.AddCurveToCategory(
                    "Player1",
                    new DoubleVector2(prevPoint.x + 0.4f, prevPoint.y),
                    new DoubleVector2(recordedPoint[i].x - 0.4f, recordedPoint[i].y),
                    new DoubleVector2(recordedPoint[i]),
                    -1f
                );
            }
        }

        void makeInput()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                yInput += 0.1f;
            }
            else
            {
                yInput -= 0.1f;
            }

            if (yInput < 0)
            {
                yInput = 0f;
            }
            else if (yInput > 10)
            {
                yInput = 10f;
            }
        }
    }
}