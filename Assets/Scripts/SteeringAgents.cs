using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgents : MonoBehaviour
{
    protected Vector3 _velocity;
    [SerializeField] protected float _maxSpeed = 5;
    [SerializeField] protected float _maxForce = 5;

    [SerializeField] protected float _viewRadius = 5;
    [SerializeField] protected LayerMask _obstacleMask = 1 << 6;

    protected Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = targetPos - transform.position;
        desired.Normalize();
        desired *= speed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }

    protected Vector3 Seek(Vector3 targetPos)
    {
        return Seek(targetPos, _maxSpeed);
    }

    protected Vector3 Flee(Vector3 targetPos)
    {
        return -Seek(targetPos);
    }

    protected Vector3 Arrive(Vector3 targetPos)
    {
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance > _viewRadius) return Seek(targetPos);

        return Seek(targetPos, _maxSpeed * (distance / _viewRadius));
    }

    protected Vector3 ObstacleAvoidance()
    {
        if (Physics.Raycast(transform.position + transform.up * 0.5f, transform.right, _viewRadius, _obstacleMask))
        {
            Vector3 desired = -transform.up * _maxSpeed;

            return CalculateSteering(desired);
        }
        else if (Physics.Raycast(transform.position - transform.up * 0.5f, transform.right, _viewRadius, _obstacleMask))
        {
            Vector3 desired = transform.up * _maxSpeed;

            return CalculateSteering(desired);
        }
        else return default;
    }

    protected Vector3 Pursuit(SteeringAgents agent)
    {
        //pos + velocity * tiempo
        Vector3 futurePos = agent.transform.position + agent._velocity;
        return Seek(futurePos);
    }

    protected Vector3 Evade(SteeringAgents agent)
    {
        return -Pursuit(agent);
    }

    #region Flocking

    protected Vector3 Cohesion(List<SteeringAgents> agents)
    {
        //Promedio de posiciones de agentes de locales.
        Vector3 desired = Vector3.zero;
        int count = 0;
        foreach (var item in agents)
        {
            if (item == this) continue;
            if (Vector3.Distance(transform.position, item.transform.position) > _viewRadius)
                continue;

            desired += item.transform.position;
            count++;
        }
        if (count == 0) return Vector3.zero;

        //Promedio = Suma / Cant.
        desired /= count;
        return Seek(desired);
    }



    protected Vector3 Separation(List<SteeringAgents> agents)
    {
        Vector3 desired = Vector3.zero;
        foreach (var item in agents)
        {
            if (item == this) continue;
            Vector3 dist = item.transform.position - transform.position;
            if (dist.sqrMagnitude > _viewRadius * _viewRadius) continue;
            desired += dist;
        }
        if (desired == Vector3.zero) return desired;
        desired *= -1;
        return CalculateSteering(desired.normalized * _maxSpeed);
    }

    protected Vector3 Alignment(List<SteeringAgents> agents)
    {
        //Promedio de posiciones de agentes de locales.
        Vector3 desired = Vector3.zero;
        int count = 0;
        foreach (var item in agents)
        {
            if(item == this) continue;
            if (Vector3.Distance(transform.position, item.transform.position) > _viewRadius) continue;

            desired += item._velocity;
            count++;
        }
        if (count == 0) return Vector3.zero;

        desired /= count;

        return CalculateSteering(desired.normalized * _maxSpeed);
    }


    #endregion


    protected void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    protected void Move()
    {
        //movimiento
        transform.position += _velocity * Time.deltaTime;

        //rotacion
        if (_velocity != Vector3.zero) transform.right = _velocity;
    }

    protected Vector3 CalculateSteering(Vector3 desired)
    {
        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);
        return steering;
    }

    protected Vector3 CalculateFuturePos(SteeringAgents agent)
    {
        return agent.transform.position + agent._velocity;
    }

    public void RestartPosition()
    {
        transform.position = Vector3.zero;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Vector3 originA = transform.position + transform.up * 0.5f;
        Vector3 originB = transform.position - transform.up * 0.5f;

        Gizmos.DrawLine(originA, originA + transform.right * _viewRadius);
        Gizmos.DrawLine(originB, originB + transform.right * _viewRadius);

    }
}